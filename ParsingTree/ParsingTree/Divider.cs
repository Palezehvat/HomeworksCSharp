namespace ParsingTree;

// A class that implements division
public class Divider : Operator
{
    private double delta = 0.0000001;

    // Keeps the division sign in itself
    public Divider(char symbol) : base(symbol) {}

    // Counts the division of two numbers on each other
    public override double Calcuate(double firstValue, double secondValue)
    {
        if (secondValue - Math.Abs(secondValue) < delta)
        {
            throw new ArgumentException();
        }
        return firstValue / secondValue;
    }

    // Displays a division sign with two spaces on the screen
    public override void Print() => Console.Write(" / ");
}
