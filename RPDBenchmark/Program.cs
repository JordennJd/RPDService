using RPDSerice.RPDGenerator.Implementation;
using RPDSerice.RPDGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
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

        string jsonRPD = "{"; // Начало JSON объекта
        for (int i = 0; i < 50; i++)
        {
            // Добавляем элементы, используя двойные кавычки и убираем запятую для
            // последнего элемента
            jsonRPD += $"\n\"{{{{TEXT_{i}}}}}\":\"ПРОВЕРКА {i}\",";
        }
        jsonRPD  += $@"
  ""{{{{TABLE_1}}}}"": [
    [""""],
    [""""],
    [""ТАБЛИЦА_1""],
    [""""],
    [""ТАБЛИЦА_1""],
    [""""],
    [""ТАБЛИЦА_1""],
    [""""],
    [""ТАБЛИЦА_1""]],
";

        jsonRPD += $@"
  ""{{{{TABLE_2}}}}"": [
    ["""", ""ТАБЛИЦА_1""],
    ["""", ""ТАБЛИЦА_1""],
    ["""", ""ТАБЛИЦА_1""],
    ["""", ""ТАБЛИЦА_1""],
    ["""", ""ТАБЛИЦА_1""]]
}}
";

        // Конец JSON объекта
        Console.WriteLine(jsonRPD);
        // Вызываем метод для генерации документа
        byte[] documentBytes = RPDGenerator.GetRPDPdfBytes(jsonRPD);

        // Здесь вы можете добавить код для сохранения byte[] в файл, если это
        // необходимо, чтобы проверить результат в виде документа Word.
        string outputPath = "output.pdf";
        System.IO.File.WriteAllBytes(outputPath, documentBytes);

        Console.WriteLine($"Документ успешно создан и сохранен как {outputPath}");
    }
}
