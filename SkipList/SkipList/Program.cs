namespace SkipList;

class Program
{
    public static void Main(string[] args)
    {
        var list = new SkipList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        var listForCheck = new List<int> { 1, 2, 3 };
        int i = 0;
        foreach (var item in list)
        {
        }
    }
}