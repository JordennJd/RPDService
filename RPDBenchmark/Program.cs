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

        // Пример JSON для
        // тестированиястированияваниястирования�рованияваниястирования
        RPD rpd = new RPD
        {
            CriticalInfo =
              new CriticalInfo
              {

                  Faculty = "Высшая черная магия",
                  Zach = "зачетно",
                  SpecialtyNumber = "9 и 3/4",
                  SPZ = "Хз че это",
                  FO = "фууууу",
                  GroupName = "М211",
                  Name = "ЛЕХА",
                  NumberOfDepartament =
                    "ФПТИ",
                  TypeOfCourseProject =
                    "Курсовик кста надо делать(",
                  CountOfHourLecture = "9999999",
                  CountOfHourPractice = "0",
                  CountOfHourLab = "-9999999",
                  CountOfHourCourseProject = "ПОМОГИТЕ АААА!!!!",
                  
                  ExamHours = "Тихо тихо",
                  SRS = "Спаси Россию Сохрани",
                  TypeOfControl = "Контролируют знатно",
                CountOfHourCourseWork =
                    "Заставляют датасеты размечать",
              } ,
            RpdInfo =
                  new RpdInfo
                  {
                      CharacteristicsOfTheSubjectArea =
                        "Не ну это вообще мрак, зачем вам это, лучше пейте пиво и играйте в Dota2, Комплексный анализ кайф, не надо тут мне",
                      LearningGoals = "Ворваться с ноги",
                      RequaredOrNotRequiared = "50/50",
                      DirPosAcadDegree= "Супер важный",
                      Initials = "В.В. ...",
                      CreatorInitials = "На гения",
                      TheNameOfTheOrientation = "Ну вы поняли",
                      CreatorDegree = "Без должностной",
                      HeadDegree = "Не супер важный",
                      HeadInitials = "Я",
                      RespDegree = "+ rep",
                      RespInitials = "- rep",
                      ViceDegree = "Его нет",
                      ViceInitials = "Говорю же что нет",
                      Program = "На первом канале",
                      ZachHours = "0"
                  }
        

        };
        Console.WriteLine(Environment.MachineName);
        // Конец JSON объекта
        // Вызываем метод для генерации документа
        byte[] documentBytes = RPDGenerator.GetRPDPdfBytes(rpd);

        // Здесь вы можете добавить код для
        // сохранения byte[] в файл, если это
        // необходимо, чтобы проверить результат в
        // виде документа Word.
        string outputPath = "output.pdf";
        System.IO.File.WriteAllBytes(outputPath, documentBytes);

        Console.WriteLine(
            $"Документ успешно создан и сохранен как {outputPath}");
    }
}
