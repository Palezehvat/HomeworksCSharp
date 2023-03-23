namespace LZW;

using System;

class Program
{
    public static void Main(string[] args)
    {
        var lzw = new LZW();
        var isCorrect = lzw.LzwAlgorithm(args[0], args[1]);
        if (!isCorrect)
        {
            Console.WriteLine("Problems...");
            return;
        }
    }
}