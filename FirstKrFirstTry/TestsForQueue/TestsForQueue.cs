namespace TestsForQueue;

using PriorityQueue;

public class Tests
{
    Queue queue;

    [SetUp]
    public void Setup()
    {
        queue = new Queue();
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldBeEmptyWhenCreated(Queue queue)
    {
        Assert.True(queue.IsEmpty());
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldThrowExceptionWhenDeletingFromAnEmptyQueue(Queue queue)
    {
        Assert.Throws<NullReferenceException>(() => queue.Dequeue());
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldNotBeEmptyWhenAddedSomething(Queue queue)
    {
        queue.Enqueue(12, 1);
        Assert.False(queue.IsEmpty());
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldReturnValueWhenAddedSomething(Queue queue)
    {
        queue.Enqueue(12, 1);
        Assert.True(queue.Dequeue() == 12);
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldReturnValueWithTheHighestPriorityWhenAddedSomething(Queue queue)
    {
        queue.Enqueue(11, 1);
        queue.Enqueue(16, 2);
        queue.Enqueue(12, 1);
        Assert.True(queue.Dequeue() == 16);
    }

    [TestCaseSource(nameof(QueueForTest))]
    public void QueueShouldReturnFirstValueWhenPriorityTheSame(Queue queue)
    {
        queue.Enqueue(15, 1);
        queue.Enqueue(13, 2);
        queue.Enqueue(14, 2);
        Assert.True(queue.Dequeue() == 13);
    }

    private static IEnumerable<TestCaseData> QueueForTest
    => new TestCaseData[]
    {
        new TestCaseData(new Queue()),
    };
}