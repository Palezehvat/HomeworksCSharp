using ParsingTree;

Tree tree = new Tree();
Console.WriteLine("Input your string");
string? stringExpression = Console.ReadLine();
try
{
    if (stringExpression == null) 
    {
        throw new ArgumentNullException(nameof(stringExpression));
    }
    tree.TreeExpression(stringExpression);
    Console.WriteLine(tree.Calcuate());
}
catch (InvalidExpressionException)
{
    Console.WriteLine("Incorrect input");
}
catch (ArgumentException)
{
    Console.WriteLine("Try to divide by zero");
}
catch (NullReferenceException)
{
    Console.WriteLine("Try to Calcuate without tree");
}
tree.PrintExpression();