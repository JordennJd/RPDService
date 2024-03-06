using RPDSerice.RPDGenerator.Implementation;
using RPDSerice.RPDGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
using RPDSerice.Models;
namespace Benchmark;

class Benchmark
{

    public static void Main()
    {

        var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true)
                                .Build();

        // Создаем экземпляр генератора
        IRPDGenerator RPDGenerator = new RPDGenerator(configuration);

        // Пример JSON для тестирования
        RPD rpd = new RPD
        {
            CriticalInfo = new CriticalInfo
            {

                Faculty = "Высшая черная магия",
                Zach = "зачетно",
                SpecialtyNumber = "9 и 3/4",
                SPZ = "Хз че это",
                FO = "фууууу",
                GroupName = "М211",
                Name = "ЛЕХА",
                NumberOfDepartament = "Департамент всевышней надобности",
                TypeOfCourseProject = "Курсовик кста надо делать(",
                CountOfHourLecture = "9999999",
                CountOfHourPractice = "0",
                CountOfHourLab = "-9999999",
                CountOfHourCourseProject = "ПОМОГИТЕ АААА!!!!",
                CountOfHourCourseWork = "Заставляют датасеты размечать",
                ExamHours = "Тихо тихо",
                SRS = "Спаси Россию Сохрани",
                TypeOfControl = "Контролируют знатно"
            }
        };

        // Конец JSON объекта
        // Вызываем метод для генерации документа
        byte[] documentBytes = RPDGenerator.GetRPDPdfBytes(rpd);

        // Здесь вы можете добавить код для сохранения byte[] в файл, если это
        // необходимо, чтобы проверить результат в виде документа Word.
        string outputPath = "output.pdf";
        System.IO.File.WriteAllBytes(outputPath, documentBytes);

        Console.WriteLine($"Документ успешно создан и сохранен как {outputPath}");
    }
}
