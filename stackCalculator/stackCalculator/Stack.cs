namespace StackCalculator;

//Standart stack
abstract public class Stack : IOperationsWithStack
{
    // Add element to stack
    virtual public void AddElement(double value) { }

    // Remove element in stack and return deleted item
    virtual public (bool, double) RemoveElement() { return (false, 0); }

    // Print all elements
    virtual public void PrintTheElements() { }

    // Checking that the stack is empty
    virtual public bool IsEmpty() { return false; }
}