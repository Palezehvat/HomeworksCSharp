namespace ParsingTree; 
public class Divisioncs : Operator
{
    double delta = 0.0000001;

    public Divisioncs(char symbol) : base(symbol) {}

    public override double Calcuate(double firstValue, double secondValue)
    {
        if (secondValue - Math.Abs(secondValue) < delta)
        {
            throw new ArgumentException();
        }
        return firstValue / secondValue;
    }

    public override void Print()
    {
        Console.Write(" / ");
    }
}
