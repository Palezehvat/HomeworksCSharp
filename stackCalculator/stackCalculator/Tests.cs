namespace StackCalculator;

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
