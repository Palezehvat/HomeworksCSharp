namespace ParsingTree;

// A class that implements subtraction
public class Minus : Operator
{
    // Keeps a minus in itself
    public Minus(char symbol) : base(symbol) {}

    // Counts the difference of two numbers
    public override double Calcuate(double firstValue, double secondValue)
    {
        return secondValue - firstValue;
    }

    // Prints a minus sign with spaces
    public override void Print()
    {
        Console.Write(" - ");
    }
}