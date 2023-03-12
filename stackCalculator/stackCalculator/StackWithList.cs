namespace StackCalculator;

// Stack implemented on list
public class StackList : IOperationsWithStack
{
    private StackElement headStack;

    public void AddElement(double value)
    {
        var item = new StackElement(value);
        if (headStack == null)
        {
           headStack = item;
        }
        else
        {
            StackElement copy = headStack;
            headStack = item;
            headStack.next = copy;
        }
    }

    public (bool, double) RemoveElement()
    {
        if (headStack == null)
        {
            return (false, 0);
        }
        double item = headStack.valueStack;
        StackElement copy = headStack.next;
        headStack = copy;
        return (true, item);
    }

    public void PrintTheElements()
    {
        StackElement walker = headStack;
        while (walker != null)
        {
            Console.WriteLine(walker.valueStack);
            walker = walker.next;
        }
    }

    public bool IsEmpty() => headStack == null;

    private class StackElement
    {
        public StackElement(double value)
        {
            valueStack = value;
            next = null;
        }

        public double valueStack { get; set; }
        public StackElement next { get; set; }
    }
}