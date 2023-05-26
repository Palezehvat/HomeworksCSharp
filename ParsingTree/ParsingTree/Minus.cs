namespace ParsingTree;

/// <summary>
/// Subtracts from one number another
/// </summary>
public class Minus : Operator
{
    /// <summary>
    /// Inherits the method of the ancestor operator
    /// </summary>
    public Minus(char symbol) : base(symbol) {}

    /// <summary>
    /// Calculates the difference
    /// </summary>
    public override double Calcuate(double firstValue, double secondValue)
    {
        return secondValue - firstValue;
    }

    /// <summary>
    /// Prints the minus sign
    /// </summary>
    public override void Print() => Console.Write(" - ");
}