namespace PriorityQueue;

/// <summary>
/// Queue with priority
/// </summary>
public class Queue
{
    private QueueElement? Head;

    /// <summary>
    /// Add element in queue by priority
    /// </summary>
    /// <param name="element">Adding element</param>
    public void Enqueue(int element, int priority)
    {

        if (IsEmpty())
        {
            Head = new QueueElement(element, priority);
            return;
        }
        var item = new QueueElement(element, priority);
        if (Head.Priority < priority)
        {
            item.Next = Head;
            Head = item;
        }
        var walker = Head;
        while (walker.Next != null)
        {
            if (walker.Next.Priority < priority)
            {
                var data = walker.Next;
                walker.Next = item;
                item.Next = data;
                return;
            }
            walker = walker.Next;
        }
        walker.Next = item;
    }

    /// <summary>
    /// Deleting first element with the highest priority
    /// </summary>
    /// <returns>Element with the highest priority</returns>
    /// <exception cref="NullReferenceException">Exception if queue empty</exception>
    public int Dequeue()
    {
        if (IsEmpty())
        {
            throw new EmtpyQueueException();
        }
        var walker = Head.Next;
        var data = Head.Element;
        Head = walker;
        return data;
    }

    /// <summary>
    /// Checking if queue is null
    /// </summary>
    /// <returns>Returns true if queue null</returns>
    public bool IsEmpty()
    {
        return Head == null;
    }

    private class QueueElement
    {
        public QueueElement(int element, int priority){
            Element = element;
            Priority = priority;
        }


        public int Element { get; set; }

        public QueueElement Next{ get; set; }

        public int Priority { get; set; }

    }
}
