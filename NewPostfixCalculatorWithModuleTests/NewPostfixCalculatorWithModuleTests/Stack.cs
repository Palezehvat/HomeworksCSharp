namespace StackCalculator; //Класс оставил иначе с тестами неудобно работать

//Standart stack
abstract public class Stack : IStack
{
    // Add element to stack
    virtual public void Push(double value) { }

    // Remove element in stack and return deleted item
    virtual public (bool, double) Pop() { return (false, 0); }

    // Print all elements
    virtual public void PrintTheElements() { }

    // Checking that the stack is empty
    virtual public bool IsEmpty() { return false; }
}