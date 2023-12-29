using static MyNUnit.ApplicationForTests;

namespace MyNUnit;

public class ResultsTests
{
    public enum Status
    {
        Passed,
        Failed,
        Ignored
    }

    public string Name { get; }

    public long WorkTime { get; }

    public Exception? ReasonFail { get; }

    public Status StatusTest { get; }

    public ResultsTests(string name, long workTime, Exception reasonFail, Status statusTest)
    {
        Name = name;
        WorkTime = workTime;
        ReasonFail = reasonFail;
        StatusTest = statusTest;
    }

    public ResultsTests(string name, long workTime, Status statusTest)
    {
        Name = name;
        WorkTime = workTime;
        StatusTest = statusTest;
    }
}
