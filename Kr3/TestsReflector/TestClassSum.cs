namespace TestsReflector;

public class TestClassSum
{
    private int val;

    public TestClassSum()
    {
        val = 0;
    }

    public int Value
    {
        get { return val; }
        set { val = value; }
    }

    public void Add(int num)
    {
        val += num;
    }
}