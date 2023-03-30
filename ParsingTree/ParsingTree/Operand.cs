namespace ParsingTree;

public class Operand : PartOfExpression
{
    public double Calcuate(double firstValue, double secondValue)
    {
        return Number;
    }

    public void Print()
    {
        Console.Write(Number);
        Console.Write(' ');
    }

    public Operand(double number) 
    {
        Number = number;
    }

    public double Number { get; set; }
}
