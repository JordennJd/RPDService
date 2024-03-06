using RPDSerice.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
namespace RPDSerice.Inital;
public static class Initial
{
	public static void Init(IServiceProvider serviceProvider, IConfiguration configuration)
	{
		InitalDatabaseFromExcelTable(serviceProvider, configuration);
	}

	private static void InitalDatabaseFromExcelTable(IServiceProvider serviceProvider, IConfiguration configuration)
	{
		string filePath = configuration.GetValue<string>("Path:ExcelFile:" + Helper.GetMachineName());
		var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

		if (db.Flags.Count(f => f.isExcelDataInstalled) > 0) return;
		using (var package = new ExcelPackage(new FileInfo(filePath)))
		{
			var worksheet = package.Workbook.Worksheets[0];
			var rowCount = worksheet.Dimension.Rows;
			var colCount = worksheet.Dimension.Columns;
			for (int row = 2; row <= rowCount; row++)
			{
				List<string> stringData = new List<string>();
				List<bool> boolData = new List<bool>();
				for (int col = 1; col <= 16; col++)
				{
					stringData.Add(worksheet.Cells[row, col].Value.ToString());

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
					CountOfHourLecture = stringData[7],
					CountOfHourPractice = stringData[8],
					CountOfHourLab = stringData[9],
					CountOfHourCourseProject = stringData[10],
					CountOfHourCourseWork = stringData[11],
					SRS = stringData[12],
					ExamHours = stringData[13],
					TypeOfCourseProject = stringData[14],
					Zach = stringData[15],
					TypeOfControl = CriticalInfo.GetTypeOfExam(boolData[0], boolData[1], boolData[2], boolData[3]),
					
				});
				Console.WriteLine("saved");

			}
			db.Database.ExecuteSqlRaw("INSERT INTO Flags (isExcelDataInstalled) VALUES (1)");
			db.SaveChanges();
		}
	}
}