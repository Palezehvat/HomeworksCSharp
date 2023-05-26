namespace StackCalculator;

// Stack implemented on an array
public class StackWithArray : IStack
{
    private double[] stackArray;
    private int numberOfElements;
    private int sizeStack = 10;

    public StackWithArray()
    {
        stackArray = new double[sizeStack];
    }

    private bool ChangeStackSize(int size)
    {
        if (size < sizeStack)
        {
            return false;
        }
        Array.Resize(ref stackArray, size);
        return true;
    }

    public void Push(double value)
    {
        if (numberOfElements == sizeStack)
        {
            ChangeStackSize(sizeStack * 2);
        }
        stackArray[numberOfElements] = value;
        ++numberOfElements;
    }

    public (bool, double) Pop()
    {
        if (numberOfElements == 0)
        {
            return (false, 0);
        }
        double result = stackArray[numberOfElements - 1];
        --numberOfElements;
        return (true, result);
    }

    public void PrintTheElements()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            Console.WriteLine(stackArray[i]);
        }
    }

    public bool IsEmpty()
        => numberOfElements == 0;
}