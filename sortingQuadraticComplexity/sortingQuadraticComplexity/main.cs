using System;

namespace Sort
{
    class Program
    {
        public static void bubbleSort(int[] arrayOut)
        {
            for (int i = 0; i < arrayOut.Length; ++i)
            {
                for (int j = arrayOut.Length - 1; j > i; --j)
                {
                    if (arrayOut[j - 1] > arrayOut[j])
                    {
                        arrayOut[j - 1] = arrayOut[j] ^ arrayOut[j - 1];
                        arrayOut[j] = arrayOut[j] ^ arrayOut[j - 1];
                        arrayOut[j - 1] = arrayOut[j] ^ arrayOut[j - 1];
                    }
                }
            }
        }

        public static bool testSort()
        {
            int[] arrayOut = { 5, 4, 2, 3, 1 };
            bubbleSort(arrayOut);
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
            int sizeArray = Int32.Parse(inputString);
            int[] arrayOut = new int[sizeArray];
            for (int i = 0; i < arrayOut.Length; ++i)
            {
                inputString = Console.ReadLine();
                if (inputString == null)
                {
                    return;
                }
                arrayOut[i] = Int32.Parse(inputString);
            }
            bubbleSort(arrayOut);
            for (int i = 0; i < sizeArray; ++i)
            {
                Console.WriteLine(arrayOut[i]);
            }
        }
    }
}