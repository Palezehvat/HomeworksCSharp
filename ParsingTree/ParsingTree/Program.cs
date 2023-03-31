using ParsingTree;
using System;

namespace ListAndUniqueList;

class Program
{
    public static void Main(string[] args)
    {
        var tree = new Tree();
        Console.WriteLine("Input your string");
        string? stringExpression = Console.ReadLine();
        try
        {
            tree.TreeExpression(stringExpression);
            Console.WriteLine(tree.Calcuate());
        }
        catch (InvalidExpressionException)
        {
            Console.WriteLine("Incorrect input");
        }
        catch(ArgumentException)
        {
            Console.WriteLine("Try to divide by zero");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Try to Calcuate without tree");
        }
        tree.PrintExpression();
    }
}