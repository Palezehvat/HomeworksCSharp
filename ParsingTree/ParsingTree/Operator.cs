namespace ParsingTree;

/// <summary>
/// A class that includes multiply divide add and subtract
/// </summary>
abstract public class Operator : PartOfExpression
{
    /// <summary>
    /// Abstract type for an action account
    /// </summary>
    public abstract double Calcuate(double firstValue, double secondValue);

    /// <summary>
    /// Abstract type for printing characters
    /// </summary>
    public abstract void Print();

    /// <summary>
    /// Stores a symbol in itself
    /// </summary>
    public Operator(char symbol)
    {
        Symbol = symbol;
    }

    public char Symbol { get; set; }
}
