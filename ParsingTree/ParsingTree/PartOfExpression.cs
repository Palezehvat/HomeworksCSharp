namespace ParsingTree;

// Interface for implementing different parts of expressions
public interface PartOfExpression
{
    // Counts two numbers
    public double Calcuate(double firstValue, double secondValue);

    // Prints a character or number
    public void Print();
}
