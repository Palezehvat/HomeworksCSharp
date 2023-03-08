namespace Sort;

using System;

interface OperationsWithElementsStruct
{
    // Add element to struct
    void AddElement(double value);

    // Remove element in struct and return deleted item
    (bool, double) RemoveElement();

    // Print all elements
    void PrintTheElements();

    // Checking that the structure is empty
    bool IsEmpty();
}

public class StackWithArray : OperationsWithElementsStruct
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

    public void AddElement(double value)
    {
        if (numberOfElements == sizeStack)
        {
            ChangeStackSize(sizeStack + sizeStack);
        }
        stackArray[numberOfElements] = value;
        ++numberOfElements;
    }

    public (bool, double) RemoveElement()
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
        for(int i = 0; i < numberOfElements; i++)
        {
            Console.WriteLine(stackArray[i]);
        }
    }

    public bool IsEmpty()
    {
       return numberOfElements == 0;
    }
}

public class StackList : OperationsWithElementsStruct
{
    public StackList()
    {
        Head = null;
    }
    private StackElement Head;

    public void AddElement(double value)
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

    public (bool, double) RemoveElement()
    {
        if (Head == null)
        {
            return(false, 0);
        }
        double item = Head.Value;
        StackElement copy = Head.Next;
        Head = copy;
        return (true, item);
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

    public bool IsEmpty()
    {
        return Head == null;
    }

    private class StackElement
    {
        public StackElement(double value)
        {
            Value = value;
            Next = null;
        }
        public double Value { get; set; }
        public StackElement Next { get; set; }
    }
}

public class StackCalculator
{
    private double delta = 0.0000000000001;
    // Receives the input string in which the expression is written in postfix form, finds the result
    public (bool, double) ConvertToAResponse(string stringWithExpression)
    {
        StackList stackExpression = new StackList();
        int i = 0;
        while (i < stringWithExpression.Length)
        {
            if ((stringWithExpression[i] == '-' && i != stringWithExpression.Length - 1 && stringWithExpression[i + 1] != ' ') || (stringWithExpression[i] >= '0' && stringWithExpression[i] <= '9'))
            {
                bool isNeedMinus = stringWithExpression[i] == '-';
                if (isNeedMinus)
                {
                    ++i;
                }
                int multiplier = 10;
                int number = 0;
                while (i < stringWithExpression.Length && stringWithExpression[i] >= '0' && stringWithExpression[i] <= '9')
                {
                    number += stringWithExpression[i] - '0';
                    number *= multiplier;
                    ++i;
                }
                number /= 10;
                --i;
                if (isNeedMinus)
                {
                    number *= -1;
                }
                stackExpression.AddElement(number);
            }
            else if (stringWithExpression[i] != ' ')
            {
                double numberAfter = 0;
                (var isCorrect, var firstNumber) = stackExpression.RemoveElement();
                (isCorrect, var secondNumber) = stackExpression.RemoveElement();

                if (!isCorrect)
                {
                    return (false, 0);
                }

                switch (stringWithExpression[i])
                {
                    case '*':
                        numberAfter = firstNumber * secondNumber;
                        break;
                    case '+':
                        numberAfter = firstNumber + secondNumber;
                        break;
                    case '-':
                        numberAfter = secondNumber - firstNumber;
                        break;
                    case '/':
                        if (Math.Abs(firstNumber) < delta)
                        {
                            return (false, 0);
                        }
                        numberAfter = secondNumber / firstNumber;
                        break;
                }
                stackExpression.AddElement(numberAfter);
            }
            ++i;
        }
        (var isCorrectExpression, var result) = stackExpression.RemoveElement();
        if (!isCorrectExpression)
        {
            return (false, 0);
        }
        return true == stackExpression.IsEmpty() ? (true, result) : (false, 0);
    }
}

public class Test
{
    // Tests the program
    public bool TestForProgram()
    {
        StackCalculator calculator = new StackCalculator();
        (var isCorrectWork, var result) = calculator.ConvertToAResponse("123 23 +");
        if (!isCorrectWork || result != 146)
        {
            return false;
        }
        (isCorrectWork, result) = calculator.ConvertToAResponse("123 23");
        return !isCorrectWork;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Test test = new Test();
        if (test.TestForProgram())
        {
            Console.WriteLine("All tests correct");
        }
        else
        {
            Console.WriteLine("Problems...");
            return;
        }
        Console.WriteLine("Enter an example in the postfix form");
        var stringWithExpression = Console.ReadLine();
        StackCalculator calculator = new StackCalculator();
        if (stringWithExpression == null)
        {
            return;
        }
        (var isCorrectWork, var result) = calculator.ConvertToAResponse(stringWithExpression);
        if (!isCorrectWork)
        {
            Console.WriteLine("Problems with expression or you tried to divide by zero");
            return;
        }
        Console.WriteLine(result);
    }
}
