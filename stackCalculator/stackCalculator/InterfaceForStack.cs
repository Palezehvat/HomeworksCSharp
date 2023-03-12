namespace StackCalculator;

 interface IOperationsWithStack
 {
    // Add element to stack
    void AddElement(double value);

    // Remove element in stack and return deleted item
    (bool, double) RemoveElement();

    // Print all elements
    void PrintTheElements();

    // Checking that the stack is empty
    bool IsEmpty();
}