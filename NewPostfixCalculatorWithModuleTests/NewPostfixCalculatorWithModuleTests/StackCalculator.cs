namespace StackCalculator;

// Calculator that counts algebraic expressions in postfix form
public static class PostfixCalculator
{
    private const double delta = 0.0000000000001;

    // Receives the input string in which the expression is written in postfix form, finds the result
    public static (bool, double) Calculate(string stringWithExpression, IStack stackExpression)
    {
        int i = 0;
        string[] expressionArray = stringWithExpression.Split(' ');
        while (i < expressionArray.Length)
        {
            var isCorrectNumber = Int32.TryParse(expressionArray[i], out var number);
            if (isCorrectNumber)
            {
                stackExpression.Push(number);
            }
            else
            {
                if (expressionArray[i].Length != 1)
                {
                    return (false, 0);
                }
                double numberAfter = 0;
                (var isCorrect, var firstNumber) = stackExpression.Pop();

                if (!isCorrect)
                {
                    return (false, 0);
                }

                (isCorrect, var secondNumber) = stackExpression.Pop();

                if (!isCorrect)
                {
                    return (false, 0);
                }

                switch (expressionArray[i][0])
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
                    default:
                        return (false, 0);
                }
                stackExpression.Push(numberAfter);
            }
            ++i;
        }
        (var isCorrectExpression, var result) = stackExpression.Pop();
        if (!isCorrectExpression)
        {
            return (false, 0);
        }
        return stackExpression.IsEmpty() ? (true, result) : (false, 0);
    }
}