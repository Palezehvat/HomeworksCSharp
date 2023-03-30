namespace ParsingTree;

// A class of numbers for the expression tree
public class Operand : PartOfExpression
{
    // Returns the passed number
    public double Calcuate(double firstValue, double secondValue)
    {
        return Number;
    }

    // Prints a number
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
