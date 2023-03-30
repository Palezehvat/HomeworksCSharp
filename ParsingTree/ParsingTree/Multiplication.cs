namespace ParsingTree;

// A class that implements multiplication
public class Multiplication : Operator
{
    // Keeps a multiplication sign in itself
    public Multiplication(char symbol) : base(symbol) {}

    // Counts multiplication of two numbers
    public override double Calcuate(double firstValue, double secondValue)
    {
        return firstValue * secondValue;
    }

    // Displays the multiplication sign with two spaces
    public override void Print()
    {
        Console.Write(" * ");
    }
}
