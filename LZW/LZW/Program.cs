namespace LZW;

using System;

class Program
{
    public static void Main(string[] args)
    {
        var lzw = new LZW();
        char le = '©';
        var (isCorrect, compressionRatio) = lzw.LzwAlgorithm(args[0], args[1]);
        if (!isCorrect)
        {
            Console.WriteLine("Problems...");
            return;
        }
        Console.WriteLine(compressionRatio);
    }
}