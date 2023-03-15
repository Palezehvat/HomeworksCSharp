namespace StackCalculator;

public class Test
{
    // Tests the program
    public bool TestForProgram()
    {
        var calculator = new PostfixCalculator();
        var stackList = new StackList();
        (var isCorrectWork, var result) = calculator.ConvertToAResponse("123 23 +", stackList);
        if (!isCorrectWork || result != 146)
        {
            return false;
        }
        var stackArray = new StackWithArray();
        (isCorrectWork, result) = calculator.ConvertToAResponse("123 23 +", stackArray);
        if (!isCorrectWork || result != 146)
        {
            return false;
        }
        (isCorrectWork, result) = calculator.ConvertToAResponse("123 23", stackList);
        if (isCorrectWork)
        {
            return false;
        }
        (isCorrectWork, result) = calculator.ConvertToAResponse("123 23", stackArray);
        return !isCorrectWork;
    }
}
