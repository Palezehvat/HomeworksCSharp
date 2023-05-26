namespace StackCalculator;

// Stack implemented on list
public class StackWithList : IStack
{
    private StackElement? headStack;

    public void Push(double value)
    {
        headStack = new StackElement(value, headStack);
    }

    public (bool, double) Pop()
    {
        if (headStack == null)
        {
            return (false, 0);
        }
        double item = headStack.ValueStack;
        StackElement? copy = headStack.Next;
        headStack = copy;
        return (true, item);
    }

    public void PrintTheElements()
    {
        StackElement? walker = headStack;
        while (walker != null)
        {
            Console.WriteLine(walker.ValueStack);
            walker = walker.Next;
        }
    }

    public bool IsEmpty() => headStack == null;

    private class StackElement
    {
        public StackElement(double value, StackElement? next)
        {
            ValueStack = value;
            Next = next;
        }

        public double ValueStack { get; set; }
        public StackElement? Next { get; set; }
    }
}