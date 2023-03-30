namespace ParsingTree;

// A class that implements amount
public class Plus : Operator
{
    // Keeps a plus in itself
    public Plus(char symbol) : base(symbol) {}

    // Counts the Amount of two numbers
    public override double Calcuate(double firstValue, double secondValue)
    {
        return secondValue + firstValue;
    }

    // Prints a plus sign with spaces
    public override void Print()
    {
        Console.Write(" + ");
    }
}
