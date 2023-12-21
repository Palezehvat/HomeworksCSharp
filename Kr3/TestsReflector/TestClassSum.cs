namespace TestsReflector;

public class TestClassSum
{
    private int val;

    public TestClassSum(int value)
    {
        val = value;
    }

    public void Add(int num)
    {
        val += num;
    }
}