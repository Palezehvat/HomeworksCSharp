namespace ParsingTree;

/// <summary>
/// A class for dividing numbers
/// </summary>
public class Divider : Operator
{
    private double delta = 0.0000001;

    /// <summary>
    /// Inherits the ancestor's method
    /// </summary>
    /// <param name="symbol">Operator</param>
    public Divider(char symbol) : base(symbol) {}

    /// <summary>
    /// Counts the division of two numbers
    /// </summary>
    /// <exception cref="ArgumentException">Throws an exception when dividing by zero</exception>
    public override double Calcuate(double firstValue, double secondValue)
    {
        if (secondValue - Math.Abs(secondValue) < delta)
        {
            throw new ArgumentException();
        }
        return firstValue / secondValue;
    }

    /// <summary>
    /// Prints the division sign in the console
    /// </summary>
    public override void Print() => Console.Write(" / ");
}
