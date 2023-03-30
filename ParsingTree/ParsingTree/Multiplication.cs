namespace ParsingTree;
public class Multiplication : Operator
{
    public Multiplication(char symbol) : base(symbol) {}

    public override double Calcuate(double firstValue, double secondValue)
    {
        return firstValue * secondValue;
    }

    public override void Print()
    {
        Console.Write(" * ");
    }
}
