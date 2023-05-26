namespace ParsingTree;

/// <summary>
/// Counts the multiplication of two numbers
/// </summary>
public class Multiplication : Operator
{
    /// <summary>
    /// Inherits the method of the ancestor operator
    /// </summary>
    public Multiplication(char symbol) : base(symbol) {}

    /// <summary>
    /// Multiplies two numbers by each other
    /// </summary>
    public override double Calcuate(double firstValue, double secondValue)
    {
        return firstValue * secondValue;
    }

    /// <summary>
    /// Prints the multiply sign
    /// </summary>
    public override void Print() => Console.Write(" * ");
}
