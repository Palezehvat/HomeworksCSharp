namespace ParsingTree;

abstract public class Operator : PartOfExpression
{
    public abstract double Calcuate(double firstValue, double secondValue);

    public abstract void Print();

    public Operator(char symbol)
    {
        Symbol = symbol;
    }

    public char Symbol { get; set; }
}
