namespace ParsingTree;

/// <summary>
/// A class based on numbers
/// </summary>
public class Operand : PartOfExpression
{
    /// <summary>
    /// Returns a stored number
    /// </summary>
    public double Calcuate(double firstValue, double secondValue)
    {
        return Number;
    }

    /// <summary>
    /// Prints a number
    /// </summary>
    public void Print() => Console.Write(Number + ' ');
    
    /// <summary>
    /// saves a number
    /// </summary>
    public Operand(double number) 
    {
        Number = number;
    }

    public double Number { get; set; }
}
