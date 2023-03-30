namespace ParsingTree;

// A class that includes multiply divide add and subtract
abstract public class Operator : PartOfExpression
{
    // Abstract type for an action account
    public abstract double Calcuate(double firstValue, double secondValue);

    // Abstract type for printing characters
    public abstract void Print();

    public Operator(char symbol)
    {
        Symbol = symbol;
    }

    public char Symbol { get; set; }
}
