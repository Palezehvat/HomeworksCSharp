namespace Bor;

using System;

class Program
{
    public static void Main(string[] args)
    {
        var bor = new Bor();

        var string1 = "da";
        var string2 = "db";
        bool check = bor.Add(string1);
        if (check == false)
        {
            Console.WriteLine("bad news");
        }
        check = bor.Add(string2);
        if (check == false)
        {
            Console.WriteLine("bad news");
        }
        bor.Remove(string2);
        Console.WriteLine(bor.HowManyStartsWithPrefix(string1));
        Console.WriteLine(bor.Contains("da"));
    }
}