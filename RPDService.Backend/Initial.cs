using RPDSerice.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
namespace RPDSerice.Inital;
public static class Initial
{
	public static void Init(IServiceProvider serviceProvider, IConfiguration configuration)
	{
		string filePath = configuration.GetValue<string>("Path:ExcelFile:" + Environment.MachineName);
		var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
		// Путь к файлу Excel
		if (db.Flags.Count(f => f.isExcelDataInstalled) > 0) return;
		using (var package = new ExcelPackage(new FileInfo(filePath)))
		{
			// Получаем доступ к рабочему листу (первый лист в книге)
			var worksheet = package.Workbook.Worksheets[0];

			// Определяем размеры таблицы
			var rowCount = worksheet.Dimension.Rows;
			var colCount = worksheet.Dimension.Columns;



			
			// if(worksheet.Cells[228,226] != null) return;
			for (int row = 2; row <= rowCount; row++)
			{
				List<string> stringData = new List<string>();
				List<bool> boolData = new List<bool>();
				for (int col = 1; col <= 15; col++)
				{
					// Получаем значение ячейки
					stringData.Add(worksheet.Cells[row, col].Value.ToString());
					// Console.WriteLine(worksheet.Cells[row, col].Value);
				}
				for (int col = 16; col <= 19; col++)
				{
					Console.WriteLine(worksheet.Cells[row, col].Value);
					boolData.Add(worksheet.Cells[row, col].Value.ToString() == "True" ? true : false);
				}
				db.Add(new CriticalInfo()
				{
					Faculty = stringData[0],
					SpecialtyNumber = stringData[1],
					SPZ = stringData[2],
					FO = stringData[3],
					GroupName = stringData[4],
					Name = stringData[5],
					NumberOfDepartament = stringData[6],
					TypeOfCourseProject = stringData[7],
					CountOfHourLecture = stringData[8],
					CountOfHourPractice = stringData[9],
					CountOfHourLab = stringData[10],
					CountOfHourCourseProject = stringData[11],
					CountOfHourCourseWork = stringData[12],
					ExamHours = stringData[13],
					SRS = stringData[14],
					
					Test = boolData[0],
					DiffTest = boolData[1],
					KandExam = boolData[2],
					Exam = boolData[3],
				});
				Console.WriteLine("saved");

			}
			db.Database.ExecuteSqlRaw("INSERT INTO Flags (isExcelDataInstalled) VALUES (1)");
			db.SaveChanges();


		}

	}
}