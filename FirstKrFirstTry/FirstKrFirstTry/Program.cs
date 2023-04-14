namespace PriorityQueue;

public class Program
{
    public static void Main()
    {
        Queue root = new Queue();
        root.Enqueue(1, 2);
        root.Enqueue(2, 3);
        root.Enqueue(3, 4);
        root.Enqueue(4, 4);
        root.Enqueue(5, 4);
        root.Enqueue(6, 4);
    }
}