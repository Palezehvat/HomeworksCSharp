namespace StackCalculator;

// Stack implemented on an array
public class StackWithArray : Stack
{
    private double[] stackArray;
    private int numberOfElements;
    private int sizeStack = 10;

    public StackWithArray()
    {
        stackArray = new double[sizeStack];
    }

    public bool ChangeStackSize(int size)
    {
        if (size < sizeStack)
        {
            return false;
        }
        Array.Resize(ref stackArray, size);
        return true;
    }

    public override void AddElement(double value)
    {
        if (numberOfElements == sizeStack)
        {
            ChangeStackSize(sizeStack * 2);
        }
        stackArray[numberOfElements] = value;
        ++numberOfElements;
    }

    public override (bool, double) RemoveElement()
    {
        if (numberOfElements == 0)
        {
            return (false, 0);
        }
        double result = stackArray[numberOfElements - 1];
        --numberOfElements;
        return (true, result);
    }

    public override void PrintTheElements()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            Console.WriteLine(stackArray[i]);
        }
    }

    public override bool IsEmpty()
        => numberOfElements == 0;
}