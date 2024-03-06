using RPDSerice.RPDGenerator.Interfaces;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using  RPDSerice.Models;

namespace RPDSerice.RPDGenerator.Implementation;

public class RPDGenerator : IRPDGenerator
{
	string templateFilePath;
	string tempFilePath;
	string outputPdfPath;
	public RPDGenerator(IConfiguration configrution)
	{
		templateFilePath = configrution["Path:templateFilePath:" + Environment.MachineName] ??
						   "Services/RPDGenerator/RPDTemplate/Template.docx";
		tempFilePath = configrution["Path:tempFilePath:" + Environment.MachineName] ??
					   "Services/RPDGenerator/RPDTemplate/temp.docx";
		outputPdfPath = configrution["Path:outputPdfPath:" + Environment.MachineName] ??
						"Services/RPDGenerator/RPDTemplate/output.pdf";
	}

	public Byte[] GetRPDPdfBytes(RPD rpd)
	{
		try
		{
			if (!File.Exists(templateFilePath))
			{
				throw new FileNotFoundException(
					$"Шаблонный файл не найден: {templateFilePath}");
			}
			File.Copy(templateFilePath, tempFilePath, true);

			// Открытие документа
			using (WordprocessingDocument doc =
					   WordprocessingDocument.Open(tempFilePath, true))
			{
				var body = doc.MainDocumentPart.Document.Body;
				var stringProperties = typeof(CriticalInfo).GetProperties()
			.Where(prop => prop.PropertyType == typeof(string))
			.Select(prop => new {prop.Name, Value = prop.GetValue(rpd.CriticalInfo)?.ToString() });
			   foreach (var stringProperty in stringProperties) {
				   ReplacePlaceholderInText(body, $"{{{stringProperty.Name}}}" , stringProperty.Value);
			   };
			}

			byte[] pdfBytes = File.ReadAllBytes(tempFilePath);
			Console.WriteLine(pdfBytes.Count());
			return pdfBytes;
		}
		catch (Exception ex)
		{
			
			throw new InvalidOperationException(
				"Произошла ошибка при генерации PDF документа из шаблона.", ex);
		}
		finally
		{
			// Очистка: удаляем временные файлы, если они существуют
			if (File.Exists(tempFilePath))
				File.Delete(tempFilePath);
			if (File.Exists(outputPdfPath))
				File.Delete(outputPdfPath);
		}
	}

	private void ReplacePlaceholderInText(Body body, string placeholder,
										  string text)
	{
			// Поиск текстовых элементов, содержащих искомый текст
			var textsContainingSearchText = body.Descendants<Text>()
												.Where(t => t.Text.Contains(placeholder));
			foreach (var textElement in textsContainingSearchText)
			{
			   textElement.Text = textElement.Text.Replace(placeholder, text);
			}
	}

	private void AddRowsToTable(Body body, int tableIndex, JArray rowsData)
	{
		// Проверяем, что индекс таблицы находится в допустимом диапазоне
		if (tableIndex < 0 || tableIndex >= body.Elements<Table>().Count())
		{
			throw new ArgumentOutOfRangeException(nameof(tableIndex),
												  "Номер таблицы вне диапазона.");
		}

		// Получаем таблицу по индексу
		var table = body.Elements<Table>().ElementAt(tableIndex);

		if (table != null)
		{
			// Получаем текущее количество строк и столбцов в таблице
			int currentRowCount = table.Elements<TableRow>().Count();
			int currentColumnCount =
				table.Elements<TableRow>().First().Elements<TableCell>().Count();

			for (int rowIndex = 0; rowIndex < rowsData.Count; rowIndex++)
			{
				JArray rowData = (JArray)rowsData[rowIndex];

				// Добавляем новую строку, если в JSON больше строк, чем в таблице
				if (rowIndex >= currentRowCount)
				{
					
					table.Append(new TableRow());
					
				}

				var row = table.Elements<TableRow>().ElementAt(rowIndex);
				

				for (int columnIndex = 0; columnIndex < rowData.Count; columnIndex++)
				{
					string cellValue = rowData[columnIndex].ToString();

					var cell = row.Elements<TableCell>().ElementAtOrDefault(columnIndex)== null? row.AppendChild(new TableCell()): row.Elements<TableCell>().ElementAtOrDefault(columnIndex);
					var cellParagraph = cell.Elements<Paragraph>().Count()!=0 ? cell.Elements<Paragraph>().First(): cell.AppendChild(new Paragraph()); 
					var cellRun = cellParagraph.Elements<Run>().Count()!=0 ? cellParagraph.Elements<Run>().First(): cellParagraph.AppendChild(new Run()); 
					var cellText = cellRun.Elements<Text>().Count()!=0 ? cellRun.Elements<Text>().First(): cellRun.AppendChild(new Text(cellValue)); // Заменяем текст в ячейке, если ячейка JSON не пустая
					if (!string.IsNullOrEmpty(cellValue))
					{
						cellText.Text = cellValue;
					}
				}
			}
		}
	}

	private void InsertEnumerationItemsInText(Body body, string holder,
											  JArray itemsData)
	{
		var paragraphs = body.Elements<Paragraph>();
		foreach (var paragraph in paragraphs)
		{
			foreach (var run in paragraph.Elements<Run>())
			{
				foreach (var textElement in run.Elements<Text>())
				{
					if (textElement.Text.Contains(holder))
					{
						var items =
							string.Join(",\n- ", itemsData.Select(item => item.ToString()));

						// Заменяем плейсхолдер {{enum}} в тексте на сформированный список
						// элементов
						textElement.Text = textElement.Text.Replace(holder, "- " + items);
					}
				}
			}
		}
	}

	private void ConvertToPdfUsingLibreOffice(string inputPath,
											  string outputPath)
	{
		try
		{
			var startInfo = new ProcessStartInfo
			{
				FileName = @"C:\Program Files (x86)\LibreOffice4\program\soffice.exe",
				Arguments =
				  $"--convert-to pdf --outdir \"{Path.GetDirectoryName(outputPath)}\" \"{inputPath}\"",
				RedirectStandardOutput = false,
				UseShellExecute = false,
				CreateNoWindow = true
			};
			startInfo.CreateNoWindow = true;
			Console.WriteLine(startInfo.Arguments);
			var process = new Process(){ StartInfo = startInfo};
			process.Start();
			process.WaitForExit();
						var outputFileName =
				Path.GetFileNameWithoutExtension(inputPath) + ".pdf";
			var generatedPdfPath =
				Path.Combine(Path.GetDirectoryName(outputPath), outputFileName);
			if (!File.Exists(generatedPdfPath))
			{
				throw new FileNotFoundException();

					
			}
			
			File.Move(generatedPdfPath, outputPath, true);
			process.CloseMainWindow();
			process.Close();


		}
		catch (Exception ex)
		{
			// Логирование или обработка ошибки конвертации
			throw new InvalidOperationException(
				$"Ошибка при конвертации файла в PDF: {ex.Message}", ex);
		}
	}
}
