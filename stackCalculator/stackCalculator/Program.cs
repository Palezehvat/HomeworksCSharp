namespace Sort;

using System;

interface OperationsWithElementsStruct
{
    // Add element to struct
    void AddElement(char value);

    // Remove element in struct and return deleted item
    void RemoveElement(ref char item);

    // Print all elements
    void PrintTheElements();
}

public class Stack : OperationsWithElementsStruct
{
    public Stack()
    {
        Head = null;
    }
    private StackElement Head;

    public void AddElement(char value)
    {
        StackElement item = new StackElement(value);
        if (Head == null)
        {
            Head = item;
        }
        else
        {
            StackElement copy = Head;
            Head = item;
            Head.Next= copy;
        }
    }

    public void RemoveElement(ref char item)
    {
        if (Head == null)
        {
            return;
        }
        item = Head.Value;
        StackElement copy = Head.Next;
        Head = null;
        Head = copy;
    }

    public void PrintTheElements()
    {
        StackElement walker = Head;
        while (walker != null)
        {
            Console.WriteLine(walker.Value);
            walker = walker.Next;
        }
    }

    private class StackElement
    {
        public StackElement(char value)
        {
            Value = value;
            Next = null;
        }
        public char Value { get; set; }
        public StackElement Next { get; set; }
    }
}

public class StackCalculator
{
    // Receives the input string in which the expression is written in postfix form, finds the result
    public int ConvertToAResponse(string stringWithExpression)
    {

        return 0;
    }
}

public class Test
{
    // Test the program
    public bool TestForProgram()
    {
        return true;
    }
}

class Program
{
    public void Main(string[] args)
    {
        Console.WriteLine("Enter an example in the postfix form");
        var stringWithExpression = Console.ReadLine();
        StackCalculator calculator = new StackCalculator();
        if (stringWithExpression == null)
        {
            return;
        }
        int result = calculator.ConvertToAResponse(stringWithExpression);
    }
}
