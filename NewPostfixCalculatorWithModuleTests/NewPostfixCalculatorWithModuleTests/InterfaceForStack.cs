namespace StackCalculator;

// Interface for the stack
interface IStack
{
    // Add element to stack
    void Push(double value);

    // Remove element in stack and return deleted item
    (bool, double) Pop();

    // Print all elements
    void PrintTheElements();

    // Checking that the stack is empty
    bool IsEmpty();
}