using Md5;
using System.Diagnostics;

public class Program
{
    public static async Task<int> Main()
    {
        Console.WriteLine("Далее будет выполнено сравнение Md5 hash с использованием многопоточности и без неё");
        Console.WriteLine("Введите путь до дирректории");
        var pathToDirrectory = Console.ReadLine();
        if (pathToDirrectory == null )
        {
            throw new NullReferenceException();
        }
        Stopwatch stopwatchForDirrectory = new Stopwatch();
        stopwatchForDirrectory.Start();
        try
        {
            var hash = SingleMd5.CalculateDirectory(pathToDirrectory);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Проблемы с путём к дирректории");
            stopwatchForDirrectory.Stop();
            return 0;
        }
        stopwatchForDirrectory.Stop();
        Console.WriteLine();
        Console.Write("Время исполнения подсчёта hash с использованием 1 потока: ");
        Console.WriteLine(stopwatchForDirrectory.ElapsedMilliseconds);
        stopwatchForDirrectory.Reset();
        stopwatchForDirrectory.Start();
        try
        {
            var hash = await MultiThreadMD5.CalculateDirectory(pathToDirrectory);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Проблемы с путём к дирректории");
            stopwatchForDirrectory.Stop();
            return 0;
        }
        stopwatchForDirrectory.Stop();
        Console.Write("Время исполнения подсчёта hash с использованием нескольких потоков: ");
        Console.WriteLine(stopwatchForDirrectory.ElapsedMilliseconds);

        Console.WriteLine();
        Console.WriteLine("Ввдеите путь до файла");
        var pathToFile = Console.ReadLine();
        if (pathToFile == null)
        {
            throw new NullReferenceException();
        }
        Stopwatch stopwatchForFile = new Stopwatch();
        stopwatchForFile.Start();
        try
        {
            var hash = SingleMd5.CalculateFile(pathToFile);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Проблемы с путём к файлу");
            stopwatchForDirrectory.Stop();
            return 0;
        }
        stopwatchForFile.Stop();
        Console.WriteLine();
        Console.Write("Время исполнения подсчёта hash с использованием 1 потока: ");
        Console.WriteLine(stopwatchForFile.ElapsedMilliseconds);
        stopwatchForFile.Reset();
        stopwatchForFile.Start();
        try
        {
            var hash = await MultiThreadMD5.CalculateFile(pathToFile);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Проблемы с путём к файлу");
            stopwatchForDirrectory.Stop();
            return 0;
        }
        stopwatchForFile.Stop();
        Console.Write("Время исполнения подсчёта hash с использованием нескольких потоков: ");
        Console.WriteLine(stopwatchForFile.ElapsedMilliseconds);
        
        return 0;
    }
}