namespace StackCalculator;

// Stack implemented on list
public class StackList : Stack
{
    private StackElement headStack;

    public override void AddElement(double value)
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
            headStack.Next = copy;
        }
    }

    public override (bool, double) RemoveElement()
    {
        if (headStack == null)
        {
            return (false, 0);
        }
        double item = headStack.ValueStack;
        StackElement copy = headStack.Next;
        headStack = copy;
        return (true, item);
    }

    public override void PrintTheElements()
    {
        StackElement walker = headStack;
        while (walker != null)
        {
            Console.WriteLine(walker.ValueStack);
            walker = walker.Next;
        }
    }

    public override bool IsEmpty() => headStack == null;

    private class StackElement
    {
        public StackElement(double value)
        {
            ValueStack = value;
            Next = null;
        }

        public double ValueStack { get; set; }
        public StackElement Next { get; set; }
    }
}