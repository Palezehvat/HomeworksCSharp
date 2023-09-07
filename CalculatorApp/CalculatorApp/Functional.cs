namespace CalculatorApp;

public class Functional
{
    private void ChangeLabel(ref Label label, bool isTest, string stringToLabel)
    {
        if (!isTest)
        {
            label.Text = stringToLabel;
        }
    }

    /// <summary>
    /// Button for change sign number
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void SignButtonClick(ref ConditionCalculator conditionCalculator, ref string firstNumber, ref string secondNumber, ref Label MainOutputLabel, bool isTest)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                break;
            case ConditionCalculator.firstNumber:
                if (!(firstNumber != "" && firstNumber.Length == 1 && firstNumber[0] == '0'))
                {
                    firstNumber = firstNumber.Insert(0, "-");
                    ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                    conditionCalculator = ConditionCalculator.signFirstNumber;
                }
                break;
            case ConditionCalculator.signFirstNumber:
                if (firstNumber[0] != '-')
                {
                    firstNumber = firstNumber.Insert(0, "-");
                }
                else
                {
                    firstNumber = firstNumber.Substring(1);
                }
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.operation:
                if (firstNumber[0] != '-')
                {
                    secondNumber = firstNumber.Insert(0, "-");
                }
                else
                {
                    secondNumber = firstNumber.Substring(1);
                }
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.signSecondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (!(secondNumber != "" && secondNumber.Length == 1 && secondNumber[0] == '0'))
                {
                    if (secondNumber[0] != '-')
                    {
                        secondNumber = secondNumber.Insert(0, "-");
                    }
                    else
                    {
                        secondNumber = secondNumber.Substring(1);
                    }
                    ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                    conditionCalculator = ConditionCalculator.signSecondNumber;
                }
                break;
            case ConditionCalculator.signSecondNumber:
                if (secondNumber[0] != '-')
                {
                    secondNumber = secondNumber.Insert(0, "-");
                }
                else
                {
                    secondNumber = secondNumber.Substring(1);
                }
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
        }
    }

    /// <summary>
    /// Button for deletions symbols
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void DeleteButton(ref ConditionCalculator conditionCalculator, ref string firstNumber, ref string secondNumber, ref Label MainOutputLabel, bool isTest)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                break;
            case ConditionCalculator.firstNumber:
                firstNumber = firstNumber.Remove(firstNumber.Length - 1);
                if (firstNumber.Length == 0)
                {
                    conditionCalculator = ConditionCalculator.start;
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                }
                else
                {
                    ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                }
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber = firstNumber.Remove(firstNumber.Length - 1);
                if (firstNumber.Length == 0 || (firstNumber.Length == 1 && firstNumber[0] == '-'))
                {
                    firstNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                }
                else
                {
                    ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                }
                break;
            case ConditionCalculator.operation:
                break;
            case ConditionCalculator.secondNumber:
                secondNumber = secondNumber.Remove(secondNumber.Length - 1);
                if (secondNumber.Length == 0)
                {
                    conditionCalculator = ConditionCalculator.operation;
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                }
                else
                {
                    ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                }
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber = secondNumber.Remove(secondNumber.Length - 1);
                if (secondNumber.Length == 0 || (secondNumber.Length == 1 && secondNumber[0] == '-'))
                {
                    conditionCalculator = ConditionCalculator.operation;
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                }
                else
                {
                    ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                }
                break;
        }
    }

    /// <summary>
    /// Button for add comma
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void CommaButton(ref ConditionCalculator conditionCalculator, ref string firstNumber, ref string secondNumber, ref Label MainOutputLabel, bool isTest)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += "0,";
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + ',');
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                if (!firstNumber.Contains(','))
                {
                    if (firstNumber.Length == 0)
                    {
                        firstNumber += '0';
                    }
                    firstNumber += ',';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + ',');
                }
                break;
            case ConditionCalculator.signFirstNumber:
                if (!firstNumber.Contains(','))
                {
                    firstNumber += ',';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + ',');
                }
                break;
            case ConditionCalculator.operation:
                secondNumber = "0,";
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + "0,");
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (!secondNumber.Contains(','))
                {
                    secondNumber += ',';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + ',');
                }
                break;
            case ConditionCalculator.signSecondNumber:
                if (!secondNumber.Contains(','))
                {
                    secondNumber += ',';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + ',');
                }
                break;
        }
    }

    /// <summary>
    /// Button for reset calculator
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="BackOutputLabel">Back screen of elements</param>
    /// <param name="ErrorLabel">Screen behind Main screen for error messages</param>
    public void ResetButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                            ref string secondNumber, ref Label MainOutputLabel, ref Label BackOutputLabel,
                            ref Label ErrorLabel, bool isTest)
    {
        if (!isTest)
        {
            MainOutputLabel.Text = "0";
            BackOutputLabel.Text = "";
            firstNumber = "";
            secondNumber = "";
            ErrorLabel.Text = "";
        }
        conditionCalculator = ConditionCalculator.start;
    }

    /// <summary>
    /// Button for delete part of expression
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void CEButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                         ref string secondNumber, ref Label MainOutputLabel, bool isTest)
    {
        ChangeLabel(ref MainOutputLabel, isTest, "0");
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                break;
            case ConditionCalculator.firstNumber:
                firstNumber = "";
                conditionCalculator = ConditionCalculator.start;
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber = "";
                conditionCalculator = ConditionCalculator.start;
                break;
            case ConditionCalculator.operation:
                break;
            case ConditionCalculator.secondNumber:
                secondNumber = "";
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber = "";
                conditionCalculator = ConditionCalculator.operation;
                break;
        }
    }

    /// <summary>
    /// Button for division a unit by number
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="BackOutputLabel">Back screen of elements</param>
    /// <param name="ErrorLabel">Screen behind Main screen for error messages</param>
    public void UnitDividedByNumberButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                                          ref string secondNumber, ref Label MainOutputLabel, ref Label BackOutputLabel,
                                          ref Label ErrorLabel ,bool isTest)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                ChangeLabel(ref ErrorLabel, isTest, "Error");
                conditionCalculator = ConditionCalculator.start;
                break;
            case ConditionCalculator.firstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    firstNumber = "";
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    firstNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    firstNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble == 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    firstNumber = "";
                    secondNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / secondNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble == 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    firstNumber = "";
                    secondNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / secondNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
        }
    }

    /// <summary>
    /// Take root number
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="BackOutputLabel">Back screen of elements</param>
    /// <param name="ErrorLabel">Screen behind Main screen for error messages</param>
    public void TakeRootButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                               ref string secondNumber, ref Label MainOutputLabel, ref Label BackOutputLabel,
                               ref Label ErrorLabel, bool isTest)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber = "0";
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = Math.Sqrt(firstNumberDouble);
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble < 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    firstNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(firstNumberDouble);
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble < 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    firstNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(firstNumberDouble);
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = Math.Sqrt(secondNumberDouble);
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble < 0)
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    firstNumber = "";
                    secondNumber = "";
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(secondNumberDouble);
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
        }
    }

    /// <summary>
    /// Squaring number
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void SquaringButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                               ref string secondNumber, ref Label MainOutputLabel,
                               bool isTest)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber = "0";
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.firstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = firstNumberDouble * firstNumberDouble;
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = firstNumberDouble * firstNumberDouble;
                firstNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = firstNumberDouble * firstNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = secondNumberDouble * secondNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = secondNumberDouble * secondNumberDouble;
                secondNumber = result.ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
        }
    }

    /// <summary>
    /// Take procent number
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void ProcentButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                              ref string secondNumber, ref Label MainOutputLabel,
                              bool isTest)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber = "0";
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                ChangeLabel(ref MainOutputLabel, isTest, "0");
                firstNumber = "0";
                break;
            case ConditionCalculator.signFirstNumber:
                ChangeLabel(ref MainOutputLabel, isTest, "0");
                firstNumber = "0";
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumber = (firstNumberDouble * firstNumberDouble / 100).ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumberDouble = Convert.ToDouble(secondNumber);
                secondNumber = (firstNumberDouble * secondNumberDouble / 100).ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
            case ConditionCalculator.signSecondNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumberDouble = Convert.ToDouble(secondNumber);
                secondNumber = (firstNumberDouble * secondNumberDouble / 100).ToString();
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
        }
    }

    /// <summary>
    /// Work with zerro button click
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    public void ZeroButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                           ref string secondNumber, ref Label MainOutputLabel,
                           bool isTest)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += '0';
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                if (firstNumber.Contains(',') || firstNumber[0] != '0')
                {
                    firstNumber += '0';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + '0');
                }
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber += '0';
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + '0');
                break;
            case ConditionCalculator.operation:
                secondNumber = "0";
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (secondNumber != "" && (secondNumber[0] != '0' || secondNumber.Contains(',')))
                {
                    secondNumber += '0';
                    ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + '0');
                }
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber += '0';
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + '0');
                break;
        }
    }

    /// <summary>
    /// Work with numbers
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="number">Pressed number</param>
    public void WorkWithNumber(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                           ref string secondNumber, ref Label MainOutputLabel,
                           bool isTest, char number)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += number;
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                if (firstNumber[0] == '0' && !firstNumber.Contains(','))
                {
                    firstNumber = "" + number;
                }
                else
                {
                    firstNumber += number;
                }
                ChangeLabel(ref MainOutputLabel, isTest, firstNumber);
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber += number;
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + number);
                break;
            case ConditionCalculator.operation:
                secondNumber = "" + number;
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (secondNumber[0] == '0' && !secondNumber.Contains(","))
                {
                    secondNumber = "" + number;
                }
                else
                {
                    secondNumber += number;
                }
                ChangeLabel(ref MainOutputLabel, isTest, secondNumber);
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber += number;
                ChangeLabel(ref MainOutputLabel, isTest, MainOutputLabel.Text + number);
                break;
        }
    }

    private double NumbersWithOperation(char symbol, string firstNumber, string secondNumber)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        try
        {
            firstNumberDouble = Convert.ToDouble(firstNumber);
            secondNumberDouble = Convert.ToDouble(secondNumber);
        }
        catch (OverflowException)
        {
            firstNumber = "";
            secondNumber = "";
            return 0;
        }
        switch (symbol)
        {
            case '+':
                return firstNumberDouble + secondNumberDouble;
            case '-':
                return firstNumberDouble - secondNumberDouble;
            case '*':
                return firstNumberDouble * secondNumberDouble;
            case '/':
                return firstNumberDouble / secondNumberDouble;
        }
        return 0;
    }

    /// <summary>
    /// Work with pressed equal button
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="BackOutputLabel">Back screen of elements</param>
    /// <param name="ErrorLabel">Screen behind Main screen for error messages</param>
    public void EqualButton(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                            ref string secondNumber, ref Label MainOutputLabel, ref Label BackOutputLabel,
                            ref Label ErrorLabel, bool isTest)
    {
        if (conditionCalculator == ConditionCalculator.operation
         || conditionCalculator == ConditionCalculator.secondNumber
         || conditionCalculator == ConditionCalculator.signSecondNumber)
        {
            var operation = BackOutputLabel.Text;
            if (secondNumber == "")
            {
                secondNumber = firstNumber;
            }
            firstNumber = NumbersWithOperation(operation[operation.Length - 1], firstNumber, secondNumber).ToString();
            if (firstNumber == "" || firstNumber == "∞" || firstNumber == "-∞" || firstNumber == "не число")
            {
                ChangeLabel(ref MainOutputLabel, isTest, "0");
                ChangeLabel(ref BackOutputLabel, isTest, "");
                firstNumber = "";
                secondNumber = "";
                ChangeLabel(ref ErrorLabel, isTest, "Error");
                conditionCalculator = ConditionCalculator.start;
            }
            else
            {
                ChangeLabel(ref BackOutputLabel, isTest, firstNumber + operation[operation.Length - 1]);
                conditionCalculator = ConditionCalculator.operation;
            }
        }
    }

    private bool AStringOfZeros(string number)
    {
        return !number.Contains('1')
                    && !number.Contains('2')
                    && !number.Contains('3')
                    && !number.Contains('4')
                    && !number.Contains('5')
                    && !number.Contains('6')
                    && !number.Contains('7')
                    && !number.Contains('8')
                    && !number.Contains('9');
    }

    /// <summary>
    /// Work with pressed operation
    /// </summary>
    /// <param name="MainOutputLabel">Main screen of elements</param>
    /// <param name="isTest">Checking what the launch is for</param>
    /// <param name="BackOutputLabel">Back screen of elements</param>
    /// <param name="ErrorLabel">Screen behind Main screen for error messages</param>
    /// <param name="symbol">Pressed operation</param>
    public void WorkWithOperations(ref ConditionCalculator conditionCalculator, ref string firstNumber,
                                   ref string secondNumber, ref Label MainOutputLabel, ref Label BackOutputLabel,
                                   ref Label ErrorLabel, bool isTest, char symbol)
    {
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += '0';
                ChangeLabel(ref BackOutputLabel, isTest, "0" + symbol);
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.firstNumber:
                ChangeLabel(ref BackOutputLabel, isTest, MainOutputLabel.Text + symbol);
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.signFirstNumber:
                ChangeLabel(ref BackOutputLabel, isTest, MainOutputLabel.Text + symbol);
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.operation:
                ChangeLabel(ref BackOutputLabel, isTest, firstNumber + symbol);
                break;
            case ConditionCalculator.secondNumber:
                if (symbol == '/' && AStringOfZeros(secondNumber))
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    firstNumber = "";
                    secondNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                }
                else
                {
                    var result = NumbersWithOperation(symbol, firstNumber, secondNumber).ToString();
                    ChangeLabel(ref BackOutputLabel, isTest, result.ToString() + symbol);
                }
                break;
            case ConditionCalculator.signSecondNumber:
                if (symbol == '/' && AStringOfZeros(secondNumber))
                {
                    ChangeLabel(ref MainOutputLabel, isTest, "0");
                    ChangeLabel(ref BackOutputLabel, isTest, "");
                    ChangeLabel(ref ErrorLabel, isTest, "Error");
                    firstNumber = "";
                    secondNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                }
                else
                {
                    var result = NumbersWithOperation(symbol, firstNumber, secondNumber).ToString();
                    ChangeLabel(ref BackOutputLabel, isTest, result.ToString() + symbol);
                }
                break;
        }
    }
}