namespace StackCalculator;

using System;
class Program
{
    public static void Main(string[] args)
    {
        Test test = new Test();
        if (test.TestForProgram())
        {
            Console.WriteLine("All tests correct");
        }
        else
        {
            Console.WriteLine("Problems...");
            return;
        }
        Console.WriteLine("Enter an example in the postfix form");
        var stringWithExpression = Console.ReadLine();
        StackCalculator calculator = new();
        if (stringWithExpression == null)
        {
            return;
        }
        (var isCorrectWork, var result) = calculator.ConvertToAResponse(stringWithExpression);
        if (!isCorrectWork)
        {
            Console.WriteLine("Problems with expression or you tried to divide by zero");
            return;
        }
        Console.WriteLine(result);
    }
}