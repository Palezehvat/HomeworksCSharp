using StackCalculator;

Console.WriteLine("Enter an example in the postfix form");
var stringWithExpression = Console.ReadLine();

PostfixCalculator calculator = new PostfixCalculator();
if (stringWithExpression == null)
{
    return;
}

var stackList = new StackList();
(bool isCorrectWork, double result) = calculator.ConvertToAResponse(stringWithExpression, stackList);
if (!isCorrectWork)
{
    Console.WriteLine("Problems with expression or you tried to divide by zero!");
    return;
}
Console.WriteLine(result);