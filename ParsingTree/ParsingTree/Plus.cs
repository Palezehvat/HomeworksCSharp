namespace ParsingTree;

/// <summary>
/// A class that implements amount
/// </summary>
public class Plus : Operator
{
    /// <summary>
    /// Inherits the method of the ancestor of the operator
    /// </summary>
    /// <param name="symbol"></param>
    public Plus(char symbol) : base(symbol) {}

    /// <summary>
    /// Counts the Amount of two numbers
    /// </summary>
    public override double Calcuate(double firstValue, double secondValue) => secondValue + firstValue;

    /// <summary>
    /// Prints a plus sign with spaces
    /// </summary>
    public override void Print() => Console.Write(" + ");
}
