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

public partial class Calculator : Form
{

    TableLayoutPanel tableLayoutPanel;
    private ConditionCalculator conditionCalculator = ConditionCalculator.start;
    private string firstNumber = "";
    private string secondNumber = "";

    public Calculator()
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

    private bool IsInfinity()
    {
        if (firstNumber == "∞" || firstNumber == "-∞" || secondNumber == "∞" || secondNumber == "-∞")
        {
            ErrorLabel.Text = "Error";
            firstNumber = "";
            secondNumber = "";
            MainOutputLabel.Text = "0";
            BackOutputLabel.Text = "";
            conditionCalculator = ConditionCalculator.start;
            return true;
        }
        return false;
    }

    private void SignButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref MainOutputLabel, false);
    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {}


    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.DeleteButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref MainOutputLabel, false);
    }

    private void CommaButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.CommaButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref MainOutputLabel, false);
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        var functional = new Functional();
        functional.ResetButton(ref conditionCalculator, ref firstNumber,
                            ref secondNumber, ref MainOutputLabel, ref BackOutputLabel,
                            ref ErrorLabel, false);
    }

    private void CEButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.CEButton(ref conditionCalculator, ref firstNumber,
                            ref secondNumber, ref MainOutputLabel, false);
    }

    private void WorkWithOperations(char symbol)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.WorkWithOperations(ref conditionCalculator, ref firstNumber,
                               ref secondNumber, ref MainOutputLabel, ref BackOutputLabel,
                               ref ErrorLabel, false, symbol);
    }

    private void OperationButton_Click(object sender, EventArgs e)
    {
        WorkWithOperations((sender as Button)?.Text[0] ?? throw new Exception());
    }

    private void EqualButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.EqualButton(ref conditionCalculator, ref firstNumber,
                               ref secondNumber, ref MainOutputLabel, ref BackOutputLabel,
                               ref ErrorLabel, false);
    }

    private void WorkWithNumbers(char number)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.WorkWithNumber(ref conditionCalculator, ref firstNumber,
                                  ref secondNumber, ref MainOutputLabel,
                                  false, number);
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
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.ZeroButton(ref conditionCalculator, ref firstNumber,
                                  ref secondNumber, ref MainOutputLabel,
                                  false);
    }
    private void ProcentButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.ProcentButton(ref conditionCalculator, ref firstNumber,
                                  ref secondNumber, ref MainOutputLabel,
                                  false);
    }

    private void SquaringButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.SquaringButton(ref conditionCalculator, ref firstNumber,
                                  ref secondNumber, ref MainOutputLabel,
                                  false);
    }

    private void TakeRootButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.TakeRootButton(ref conditionCalculator, ref firstNumber,
                                             ref secondNumber, ref MainOutputLabel, ref BackOutputLabel,
                                             ref ErrorLabel, false);
    }

    private void UnitDividedByNumberButton_Click(object sender, EventArgs e)
    {
        if (IsInfinity())
        {
            return;
        }
        ErrorLabel.Text = "";
        var functional = new Functional();
        functional.UnitDividedByNumberButton(ref conditionCalculator, ref firstNumber,
                                             ref secondNumber, ref MainOutputLabel, ref BackOutputLabel,
                                             ref  ErrorLabel, false);
    }
}