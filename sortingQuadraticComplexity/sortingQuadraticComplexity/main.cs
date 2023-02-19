using System;
namespace Sort;

class Program
{
    //Sorts an array using the bubble method
    public static void BubbleSort(int[] arrayOut)
    {
        for (int i = 0; i < arrayOut.Length; ++i)
        {
            for (int j = arrayOut.Length - 1; j > i; --j)
            {
                if (arrayOut[j - 1] > arrayOut[j])
                {
                    (arrayOut[j - 1], arrayOut[j]) = (arrayOut[j], arrayOut[j - 1]);
                }
            }
        }
    }

    //Test to check the correctness of bubble sorting
    public static bool testSort()
    {
        int[] arrayOut = { 5, 4, 2, 3, 1 };
        BubbleSort(arrayOut);
        for (int i = 1; i < 5; ++i)
        {
            if (arrayOut[i] < arrayOut[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    static void Main(string[] args)
    {
        if (testSort())
        {
            Console.WriteLine("Tests correct");
        }
        else
        {
            Console.WriteLine("Tests failed...");
            return;
        }
        Console.WriteLine("Write size array and elements of array");
        var inputString = Console.ReadLine();
        if (inputString == null)
        {
            return;
        }
        int sizeArray = 0;
        bool isCorrectInput = Int32.TryParse(inputString, out sizeArray);
        if (!isCorrectInput)
        {
            return;
        }
        var arrayOut = new int[sizeArray];
        for (int i = 0; i < arrayOut.Length; ++i)
        {
            inputString = Console.ReadLine();
            if (inputString == null)
            {
                return;
            }
            isCorrectInput = Int32.TryParse(inputString, out arrayOut[i]);
            if (!isCorrectInput)
            {
                return;
            }
        }
        BubbleSort(arrayOut);
        for (int i = 0; i < sizeArray; ++i)
        {
            Console.WriteLine(arrayOut[i]);
        }
    }
}