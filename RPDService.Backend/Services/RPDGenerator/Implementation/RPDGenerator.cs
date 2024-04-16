using RPDSerice.RPDGenerator.Interfaces;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using RPDSerice.Models;
using MyDataStructures;

namespace RPDSerice.RPDGenerator.Implementation;

public class RPDGenerator : IRPDGenerator
{
	private BinaryTree<string> _properties;
	
	string templateFilePath;
	string tempFilePath;
	string outputPdfPath;
	public RPDGenerator(IConfiguration configrution)
	{
		templateFilePath =
			configrution["Path:templateFilePath:" + Environment.MachineName] ??
			"Services/RPDGenerator/RPDTemplate/Template.docx";
		tempFilePath =
			configrution["Path:tempFilePath:" + Environment.MachineName] ??
			"Services/RPDGenerator/RPDTemplate/temp.docx";
		outputPdfPath =
			configrution["Path:outputPdfPath:" + Environment.MachineName] ??
			"Services/RPDGenerator/RPDTemplate/output.pdf";
	}

	public Byte[] GetRPDPdfBytes(RPD rpd)
	{
		_properties.Insert(rpd.CreatorId.ToString());
		_properties.Insert(rpd.CriticalInfo.CountOfHourCourseProject);
		_properties.Insert(rpd.RpdInfoId.ToString());
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
					   WordprocessingDocument.Open(tempFilePath, true)) { 
				ReplaceOnRpd(doc, rpd.CriticalInfo );
				ReplaceOnRpd(doc, rpd.RpdInfo);
			}

			byte[] pdfBytes = File.ReadAllBytes(tempFilePath);

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
			// if (File.Exists(tempFilePath))
			//     File.Delete(tempFilePath);
			// if (File.Exists(outputPdfPath))
			//     File.Delete(outputPdfPath);
		}
	}
	private void ReplaceOnRpd<Type>(WordprocessingDocument doc, Type rpd)  where Type : class
	{
		var body = doc.MainDocumentPart.Document.Body;
		var stringProperties =
			typeof(Type)
				.GetProperties()
				.Where(prop => prop.PropertyType == typeof(string))
				.Select(prop => new
				{
					prop.Name,
					Value = prop.GetValue(rpd)?.ToString()
				});
		var listOfStringProperties =
			typeof(Type)
				.GetProperties()
				.Where(prop => prop.PropertyType.IsGenericType &&
							   prop.PropertyType.GetGenericTypeDefinition() ==
								   typeof(List<>) &&
							   prop.PropertyType.GenericTypeArguments[0] ==
								   typeof(string))
				.Select(prop => new
				{
					prop.Name,
					Value = (List<string>)prop.GetValue(rpd)
				});
		var listOfListOfStringProperties =
			typeof(Type)
				.GetProperties()
				.Where(
					prop =>
						prop.PropertyType.IsGenericType &&
						prop.PropertyType.GetGenericTypeDefinition() ==
							typeof(List<>) &&
						prop.PropertyType.GenericTypeArguments[0].IsGenericType &&
						prop.PropertyType.GenericTypeArguments[0]
								.GetGenericTypeDefinition() == typeof(List<>) &&
						prop.PropertyType.GenericTypeArguments[0]
								.GenericTypeArguments[0] == typeof(string))
				.Select(prop => new
				{
					prop.Name,
					Value = (List<List<string>>)prop.GetValue(
										  rpd)
				});

		foreach (var stringProperty in stringProperties)
		{
			if(stringProperty.Value == null) continue;
			ReplacePlaceholderInText(body, $"{{{stringProperty.Name}}}",
									 stringProperty.Value);
		}
		foreach (var listOfStringProperty in listOfStringProperties)
		{

			if(listOfStringProperty.Value == null) continue;
			InsertEnumerationItemsInText(doc, $"{{{listOfStringProperty.Name}}}",
										 listOfStringProperty.Value);
		}
		foreach (var listOfListOfStringProperty in listOfListOfStringProperties)
		{
			if(listOfListOfStringProperty.Value ==null) continue;
			AddRowsToTable(
				body,
				FindTableByPlaceholder(doc, $"{{{listOfListOfStringProperty.Name}}}"),
				listOfListOfStringProperty.Value);
		}
	}
	private void ReplacePlaceholderInText(Body body, string placeholder,
										  string text)
	{
		// Поиск текстовых элементов, содержащих искомый текст
		var textsContainingSearchText =
			body.Descendants<Text>().Where(t => t.Text.Contains(placeholder));
		foreach (var textElement in textsContainingSearchText)
		{
			textElement.Text = textElement.Text.Replace(placeholder, text);
		}
	}

	private void AddRowsToTable(Body body, Table table,
								List<List<string>> rowsData)
	{

		// Получаем таблицу по индексу

		if (table != null)
		{
			// Получаем текущее количество строк и столбцов в таблице
			int currentRowCount = table.Elements<TableRow>().Count();
			int currentColumnCount =
				table.Elements<TableRow>().First().Elements<TableCell>().Count();

			for (int rowIndex = 0; rowIndex < rowsData.Count; rowIndex++)
			{
				List<string> rowData = rowsData[rowIndex];

				// Добавляем новую строку, если в JSON больше строк, чем в таблице
				if (rowIndex >= currentRowCount)
				{

					table.Append(new TableRow());
				}

				var row = table.Elements<TableRow>().ElementAt(rowIndex);

				for (int columnIndex = 0; columnIndex < rowData.Count; columnIndex++)
				{
					string cellValue = rowData[columnIndex].ToString();

					var cell =
						row.Elements<TableCell>().ElementAtOrDefault(columnIndex) == null
							? row.AppendChild(new TableCell())
							: row.Elements<TableCell>().ElementAtOrDefault(columnIndex);
					var cellParagraph = cell.Elements<Paragraph>().Count() != 0
											? cell.Elements<Paragraph>().First()
											: cell.AppendChild(new Paragraph());
					var cellRun = cellParagraph.Elements<Run>().Count() != 0
									  ? cellParagraph.Elements<Run>().First()
									  : cellParagraph.AppendChild(new Run());
					var cellText = cellRun.Elements<Text>().Count() != 0
									   ? cellRun.Elements<Text>().First()
									   : cellRun.AppendChild(new Text(
											 cellValue)); // Заменяем текст в ячейке, если
														  // ячейка JSON не пустая
					if (!string.IsNullOrEmpty(cellValue))
					{
						cellText.Text = cellValue;
					}
				}
			}
		}
	}

	private void InsertEnumerationItemsInText(WordprocessingDocument doc,
											  string holder,
											  List<string> itemsData)
	{
		// Находим все элементы Text, содержащие плейсхолдер.
		var paragraphsContainingHolder =
			doc.MainDocumentPart.Document.Body.Descendants<Paragraph>()
				.Where(p => p.InnerText.Contains(holder))
				.ToList();

		foreach (var paragraph in paragraphsContainingHolder)
		{
			var parent =
				paragraph
					.Parent; // Получаем родительский элемент для текущего Paragraph

			// Создаём новый абзац для каждого элемента списка.
			foreach (var item in itemsData)
			{
				var newParagraph = new Paragraph(new Run(new Text("- " + item)));
				parent.InsertAfter(newParagraph, paragraph);
			}

			// Удаление исходного абзаца с плейсхолдером, если необходимо.
			// Можно также заменить текст внутри плейсхолдера на первый элемент
			// списка, если не хотите удалять весь абзац.
			paragraph.Remove();
		}
	}

	public static Table FindTableByPlaceholder(WordprocessingDocument document,
											   string placeholder)
	{
		// Получаем основную часть документа
		var mainPart = document.MainDocumentPart;

		// Перебираем все таблицы в документе
		var tables = mainPart.Document.Body.Elements<Table>();
		foreach (var table in tables)
		{
			// Перебираем все строки в каждой таблице
			foreach (var row in table.Elements<TableRow>())
			{
				// Перебираем все ячейки в каждой строке
				foreach (var cell in row.Elements<TableCell>())
				{
					// Проверяем, содержит ли текст в ячейке искомый плейсхолдер
					if (cell.InnerText.Contains(placeholder))
					{
						return table;
					}
				}
			}
		}

		// Возвращаем null, если таблица с таким плейсхолдером не найдена
		return null;
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
			var process = new Process() { StartInfo = startInfo };
			process.Start();
			process.WaitForExit();
			var outputFileName = Path.GetFileNameWithoutExtension(inputPath) + ".pdf";
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
