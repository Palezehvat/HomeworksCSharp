using StackCalculator;

Console.WriteLine("Enter an example in the postfix form");
var stringWithExpression = Console.ReadLine();

if (stringWithExpression == null)
{
    return;
}

var stackList = new StackWithList();
(bool isCorrectWork, double result) = PostfixCalculator.Calculate(stringWithExpression, stackList);
if (!isCorrectWork)
{
    Console.WriteLine("Problems with expression or you tried to divide by zero!");
    return;
}
Console.WriteLine(result);