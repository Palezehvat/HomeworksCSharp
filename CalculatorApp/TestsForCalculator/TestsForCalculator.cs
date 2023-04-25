namespace TestsForCalculator;

using CalculatorApp;
using System.Reflection.Emit;

public class Tests
{
    Functional functional;
    ConditionCalculator conditionCalculator;
    System.Windows.Forms.Label mainOutputLabel;
    System.Windows.Forms.Label backOutputLabel;
    System.Windows.Forms.Label errorLabel;
    [SetUp]
    public void Setup()
    {
        functional = new Functional();
        conditionCalculator = ConditionCalculator.start;
        mainOutputLabel = new System.Windows.Forms.Label();
        backOutputLabel = new System.Windows.Forms.Label();
    }

    [Test]
    public void CEButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "1";
        var secondNumber = "2";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.CEButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber, Is.EqualTo(""));
    }

    [Test]
    public void CEButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "1";
        var secondNumber = "2";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.CEButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber, Is.EqualTo(""));
    }

    [Test]
    public void SquaringButtonShouldWorkCorrectly()
    {
        var firstNumber = "9";
        var secondNumber = "1";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.SquaringButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "81" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void ProcentButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "9";
        var secondNumber = "1";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.ProcentButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "0" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void ProcentButtonShouldWorkCorrectlyWithSecondNumberAndDifficultProcent()
    {
        var firstNumber = "1000";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.ProcentButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber == "150" && conditionCalculator == ConditionCalculator.secondNumber);
    }

    [Test]
    public void SignButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "100";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "-100" && conditionCalculator == ConditionCalculator.signFirstNumber);
    }

    [Test]
    public void SignButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "100";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber == "-15" && conditionCalculator == ConditionCalculator.signSecondNumber);
    }

    [Test]
    public void SignButtonShouldWorkCorrectlyThanTwicedUsed()
    {
        var firstNumber = "100";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "100" && conditionCalculator == ConditionCalculator.signFirstNumber);
    }

    [Test]
    public void SignButtonShouldWorkCorrectlyWithZero()
    {
        var firstNumber = "0";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.start;
        functional.SignButtonClick(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "0" && conditionCalculator == ConditionCalculator.start);
    }

    [Test]
    public void TakeRootButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "100";
        var secondNumber = "15";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.TakeRootButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(firstNumber == "10" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void TakeRootButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "100";
        var secondNumber = "225";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.TakeRootButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(secondNumber == "15" && conditionCalculator == ConditionCalculator.secondNumber);
    }

    [Test]
    public void TakeRootButtonShouldWorkCorrectlyWithZero()
    {
        var firstNumber = "0";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.start;
        functional.TakeRootButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(firstNumber == "0" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void UnitDividedByNumberButtonShouldWorkCorrectlyWithfFirstNumber()
    {
        var firstNumber = "10";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.UnitDividedByNumberButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(firstNumber == "0.1" && conditionCalculator == ConditionCalculator.firstNumber);
    }
    
    [Test]
    public void UnitDividedByNumberButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "10";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.UnitDividedByNumberButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(secondNumber == "0.2" && conditionCalculator == ConditionCalculator.secondNumber);
    }

    [Test]
    public void DeleteButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "10";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.DeleteButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "1" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void DeleteButtonShouldWorkCorrectlyWithDeleteAllFirstNumber()
    {
        var firstNumber = "1";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.DeleteButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "" && conditionCalculator == ConditionCalculator.start);
    }

    [Test]
    public void DeleteButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "1";
        var secondNumber = "50";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.DeleteButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber == "5" && conditionCalculator == ConditionCalculator.secondNumber);
    }

    [Test]
    public void DeleteButtonShouldWorkCorrectlyWithDeleteAllSecondNumber()
    {
        var firstNumber = "1";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.DeleteButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber == "" && conditionCalculator == ConditionCalculator.operation);
    }

    [Test]
    public void AddNumberButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "1";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.WorkWithNumber(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true, '2');
        Assert.That(firstNumber == "12" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void AddNumberButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "1";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.WorkWithNumber(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true, '2');
        Assert.That(secondNumber == "52" && conditionCalculator == ConditionCalculator.secondNumber);
    }

    [Test]
    public void EqualButtonShouldWorkCorrectly()
    {
        var firstNumber = "12";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.secondNumber;
        backOutputLabel.Text = "12+";
        functional.EqualButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(firstNumber == "17" && conditionCalculator == ConditionCalculator.operation);
    }

    [Test]
    public void EqualButtonShouldWorkCorrectlyWithStartCondition()
    {
        var firstNumber = "";
        var secondNumber = "";
        conditionCalculator = ConditionCalculator.start;
        functional.EqualButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, ref backOutputLabel, ref errorLabel, true);
        Assert.That(firstNumber == "" && conditionCalculator == ConditionCalculator.start);
    }

    [Test]
    public void ZeroShouldWorkCorrectly()
    {
        var firstNumber = "0";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.ZeroButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "0" && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void CommaButtonShouldWorkCorrectlyWithFirstNumber()
    {
        var firstNumber = "0";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.firstNumber;
        functional.CommaButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(firstNumber == "0," && conditionCalculator == ConditionCalculator.firstNumber);
    }

    [Test]
    public void CommaButtonShouldWorkCorrectlyWithSecondNumber()
    {
        var firstNumber = "0";
        var secondNumber = "5";
        conditionCalculator = ConditionCalculator.secondNumber;
        functional.CommaButton(ref conditionCalculator, ref firstNumber, ref secondNumber, ref mainOutputLabel, true);
        Assert.That(secondNumber == "5," && conditionCalculator == ConditionCalculator.secondNumber);
    }
}