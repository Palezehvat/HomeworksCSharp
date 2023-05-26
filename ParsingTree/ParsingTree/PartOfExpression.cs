namespace ParsingTree;

/// <summary>
/// Interface for implementing different parts of expressions
/// </summary>
public interface PartOfExpression
{
    /// <summary>
    /// Counts two numbers
    /// </summary>
    public double Calcuate(double firstValue, double secondValue);

    /// <summary>
    /// Prints a character or number
    /// </summary>
    public void Print();
}
