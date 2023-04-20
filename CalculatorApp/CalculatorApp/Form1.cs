using System.Net.WebSockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorApp;

public enum ConditionCalculator
{
    start,
    firstNumber,
    signFirstNumber,
    operation,
    secondNumber,
    signSecondNumber
}

public partial class Form1 : Form
{

    TableLayoutPanel tableLayoutPanel;
    private ConditionCalculator conditionCalculator = ConditionCalculator.start;
    private string firstNumber = "";
    private string secondNumber = "";

    public Form1()
    {
        InitializeComponent();

        tableLayoutPanel = new TableLayoutPanel
        {
            Parent = this,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
    }

    private void Form1_Load(object sender, EventArgs e) {}

    private void SignButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                break;
            case ConditionCalculator.firstNumber:
                if (!(firstNumber != "" && firstNumber.Length == 1 && firstNumber[0] == '0'))
                {
                    firstNumber = firstNumber.Insert(0, "-");
                    MainOutputLabel.Text = firstNumber;
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
                MainOutputLabel.Text = firstNumber;
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
                MainOutputLabel.Text = secondNumber;
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
                    MainOutputLabel.Text = secondNumber;
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
                MainOutputLabel.Text = secondNumber;
                break;
        }
    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {}


    private void DeleteButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                break;
            case ConditionCalculator.firstNumber:
                firstNumber = firstNumber.Remove(firstNumber.Length - 1);
                if (firstNumber.Length == 0)
                {
                    conditionCalculator = ConditionCalculator.start;
                    MainOutputLabel.Text = "0";
                }
                else
                {
                    MainOutputLabel.Text = firstNumber;
                }
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber = firstNumber.Remove(firstNumber.Length - 1);
                if (firstNumber.Length == 0 || (firstNumber.Length == 1 && firstNumber[0] == '-'))
                {
                    firstNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                    MainOutputLabel.Text = "0";
                }
                else
                {
                    MainOutputLabel.Text = firstNumber;
                }
                break;
            case ConditionCalculator.operation:
                break;
            case ConditionCalculator.secondNumber:
                secondNumber = secondNumber.Remove(secondNumber.Length - 1);
                if (secondNumber.Length == 0)
                {
                    conditionCalculator = ConditionCalculator.operation;
                    MainOutputLabel.Text = "0";
                }
                else
                {
                    MainOutputLabel.Text = secondNumber;
                }
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber = secondNumber.Remove(secondNumber.Length - 1);
                if (secondNumber.Length == 0 || (secondNumber.Length == 1 && secondNumber[0] == '-'))
                {
                    conditionCalculator = ConditionCalculator.operation;
                    MainOutputLabel.Text = "0";
                }
                else
                {
                    MainOutputLabel.Text = secondNumber;
                }
                break;
        }
    }

    private void CommaButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += "0,";
                MainOutputLabel.Text += ',';
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
                    MainOutputLabel.Text += ",";
                }
                break;
            case ConditionCalculator.signFirstNumber:
                if (!firstNumber.Contains(','))
                {
                    firstNumber += ',';
                    MainOutputLabel.Text += ",";
                }
                break;
            case ConditionCalculator.operation:
                secondNumber = "0,";
                MainOutputLabel.Text = "0,";
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (!secondNumber.Contains(','))
                {
                    secondNumber += ',';
                    MainOutputLabel.Text += ",";
                }
                break;
            case ConditionCalculator.signSecondNumber:
                if (!secondNumber.Contains(','))
                {
                    secondNumber += ',';
                    MainOutputLabel.Text += ",";
                }
                break;
        }
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        MainOutputLabel.Text = "0";
        BackOutputLabel.Text = "";
        firstNumber = "";
        secondNumber = "";
        ErrorLabel.Text = "";
        conditionCalculator = ConditionCalculator.start;
    }

    private void CEButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        MainOutputLabel.Text = "0";
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

    private double NumbersWithOperation(char symbol)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        try
        {
            firstNumberDouble = Convert.ToDouble(firstNumber);
            secondNumberDouble = Convert.ToDouble(secondNumber);
        }
        catch(OverflowException)
        {
            firstNumber = "";
            secondNumber = "";
            return 0;
        }
        switch(symbol)
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

    private bool AStringOfZeros(string number)
    {
        return         !number.Contains('1')
                    && !number.Contains('2')
                    && !number.Contains('3')
                    && !number.Contains('4')
                    && !number.Contains('5')
                    && !number.Contains('6')
                    && !number.Contains('7')
                    && !number.Contains('8')
                    && !number.Contains('9');
    }

    private void WorkWithOperations(char symbol)
    {
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += '0';
                BackOutputLabel.Text = "0" + symbol;
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.firstNumber:
                BackOutputLabel.Text = MainOutputLabel.Text + symbol;
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.signFirstNumber:
                BackOutputLabel.Text = MainOutputLabel.Text + symbol;
                conditionCalculator = ConditionCalculator.operation;
                break;
            case ConditionCalculator.operation:
                BackOutputLabel.Text = firstNumber.ToString() + symbol;
                break;
            case ConditionCalculator.secondNumber:
                if (symbol == '/' && AStringOfZeros(secondNumber))
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                }
                else
                {
                    var result = NumbersWithOperation(symbol).ToString();
                    if (firstNumber == "" || secondNumber == "")
                    {
                        MainOutputLabel.Text = "0";
                        BackOutputLabel.Text = "";
                        return;
                    }
                    BackOutputLabel.Text = result.ToString() + symbol;
                }
                break;
            case ConditionCalculator.signSecondNumber:
                if (symbol == '/' && AStringOfZeros(secondNumber))
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    conditionCalculator = ConditionCalculator.start;
                }
                else
                {
                    var result = NumbersWithOperation(symbol).ToString();
                    if (firstNumber == "" || secondNumber == "")
                    {
                        MainOutputLabel.Text = "0";
                        BackOutputLabel.Text = "";
                        return;
                    }
                    BackOutputLabel.Text = result.ToString() + symbol;
                }
                break;
        }
    }

    private void PlusButton_Click(object sender, EventArgs e)
    {
        WorkWithOperations('+');
    }

    private void SubtractButton_Click(object sender, EventArgs e)
    {
        WorkWithOperations('-');
    }

    private void MultiplyButton_Click(object sender, EventArgs e)
    {
        WorkWithOperations('*');
    }

    private void DivisionButton_Click(object sender, EventArgs e)
    {
        WorkWithOperations('/');
    }

    private void EqualButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        if (conditionCalculator == ConditionCalculator.operation
         || conditionCalculator == ConditionCalculator.secondNumber
         || conditionCalculator == ConditionCalculator.signSecondNumber)
        {
            var operation = BackOutputLabel.Text;
            if (secondNumber == "")
            {
                secondNumber = firstNumber;
            }
            firstNumber = NumbersWithOperation(operation[operation.Length - 1]).ToString();
            if (firstNumber == "" || firstNumber == "∞" || firstNumber == "-∞" || firstNumber == "не число")
            {
                MainOutputLabel.Text = "0";
                BackOutputLabel.Text = "";
                firstNumber = "";
                secondNumber = "";
                ErrorLabel.Text = "Error";
                conditionCalculator = ConditionCalculator.start;
            }
            else
            {
                BackOutputLabel.Text = firstNumber + operation[operation.Length - 1];
                conditionCalculator = ConditionCalculator.operation;
            }
        }
    }

    private void WorkWithNumbers(char number)
    {
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber += number;
                MainOutputLabel.Text = firstNumber;
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
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber += number;
                MainOutputLabel.Text += number;
                break;
            case ConditionCalculator.operation:
                secondNumber = "" + number;
                MainOutputLabel.Text = secondNumber;
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
                MainOutputLabel.Text = secondNumber;
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber += number;
                MainOutputLabel.Text += number;
                break;
        }
    }

    private void OneButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('1');
    }

    private void TwoButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('2');
    }

    private void ThreeButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('3');
    }

    private void FourButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('4');
    }

    private void FiveButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('5');
    }

    private void SixButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('6');
    }

    private void SevenButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('7');
    }

    private void EightButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('8');
    }

    private void NineButton_Click(object sender, EventArgs e)
    {
        WorkWithNumbers('9');
    }

    private void ZeroButton_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
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
                    MainOutputLabel.Text += '0';
                }
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumber += '0';
                MainOutputLabel.Text += '0';
                break;
            case ConditionCalculator.operation:
                secondNumber = "0";
                MainOutputLabel.Text = secondNumber;
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                if (secondNumber != "" && (secondNumber[0] != '0' || secondNumber.Contains(',')))
                {
                    secondNumber += '0';
                    MainOutputLabel.Text += '0';
                }
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumber += '0';
                MainOutputLabel.Text += '0';
                break;
        }
    }
    private void ProcentButton_Click(object sender, EventArgs e)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                firstNumber = "0";
                conditionCalculator = ConditionCalculator.firstNumber;
                break;
            case ConditionCalculator.firstNumber:
                MainOutputLabel.Text = "0";
                firstNumber = "0";
                break;
            case ConditionCalculator.signFirstNumber:
                MainOutputLabel.Text = "0";
                firstNumber = "0";
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumber = (firstNumberDouble * firstNumberDouble / 100).ToString();
                MainOutputLabel.Text = secondNumber;
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumberDouble = Convert.ToDouble(secondNumber);
                secondNumber = (firstNumberDouble * secondNumberDouble / 100).ToString();
                MainOutputLabel.Text = secondNumber;
                break;
            case ConditionCalculator.signSecondNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                secondNumberDouble = Convert.ToDouble(secondNumber);
                secondNumber = (firstNumberDouble * secondNumberDouble / 100).ToString();
                MainOutputLabel.Text = secondNumber;
                break;
        }
    }

    private void SquaringButtom_Click(object sender, EventArgs e)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        ErrorLabel.Text = "";
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
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = firstNumberDouble * firstNumberDouble;
                firstNumber = result.ToString();
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                result = firstNumberDouble * firstNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = secondNumberDouble * secondNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = secondNumberDouble * secondNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
        }
    }

    private void TakeRootButton_Click(object sender, EventArgs e)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        ErrorLabel.Text = "";
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
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble < 0)
                {
                    MainOutputLabel.Text = "0";
                    firstNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(firstNumberDouble);
                firstNumber = result.ToString();
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble < 0)
                {
                    MainOutputLabel.Text = "0";
                    firstNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(firstNumberDouble);
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                result = Math.Sqrt(secondNumberDouble);
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble < 0)
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = Math.Sqrt(secondNumberDouble);
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
        }
    }

    private void UnitDividedByNumberButton_Click(object sender, EventArgs e)
    {
        double firstNumberDouble = 0;
        double secondNumberDouble = 0;
        double result = 0;
        ErrorLabel.Text = "";
        switch (conditionCalculator)
        {
            case ConditionCalculator.start:
                ErrorLabel.Text = "Error";
                conditionCalculator = ConditionCalculator.start;
                break;
            case ConditionCalculator.firstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    firstNumber = "";
                    MainOutputLabel.Text = "0";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                firstNumber = result.ToString();
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.signFirstNumber:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    MainOutputLabel.Text = "0";
                    firstNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                firstNumber = result.ToString();
                MainOutputLabel.Text = firstNumber;
                break;
            case ConditionCalculator.operation:
                firstNumberDouble = Convert.ToDouble(firstNumber);
                if (firstNumberDouble == 0)
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / firstNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                conditionCalculator = ConditionCalculator.secondNumber;
                break;
            case ConditionCalculator.secondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble == 0)
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / secondNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
            case ConditionCalculator.signSecondNumber:
                secondNumberDouble = Convert.ToDouble(secondNumber);
                if (secondNumberDouble == 0)
                {
                    MainOutputLabel.Text = "0";
                    BackOutputLabel.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    ErrorLabel.Text = "Error";
                    conditionCalculator = ConditionCalculator.start;
                    return;
                }
                result = 1 / secondNumberDouble;
                secondNumber = result.ToString();
                MainOutputLabel.Text = secondNumber;
                break;
        }
    }
}