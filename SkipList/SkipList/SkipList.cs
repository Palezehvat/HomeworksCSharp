namespace SkipList;

using System.Collections;

public class SkipList<T> : IList<T> where T : IComparable<T>
{
    private class List
    {
        public ElementList? element;
    }

    private class MainList
    {
        public MainList? nextList;
        public List? list;
        public int size;
    }

    private MainList? MajorList;

    public void Add(T item)
    {
        if (MajorList == null)
        {
            MajorList = new MainList();
            MajorList.list = new List();
            MajorList.list.element = new ElementList(0, item, 0);
            MajorList.size = 1;
            return;
        }

        if (MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }

        var walker = MajorList.list.element;
        var stack = new Stack<ElementList>();

        ++MajorList.size;

        while (walker != null)
        {
            if (walker.next == null)
            {
                if (walker.level == 0)
                {
                    var copy = walker.next;
                    walker.next = new ElementList(walker.position + 1, item, 0);
                    walker.next.next = copy;
                    var randomNumber = new Random();
                    int position = 1;
                    var previousLevelItem = walker.next;
                    while (randomNumber.Next(0, 2) == 0)
                    {
                        if (stack.Count > 0)
                        {
                            var walkerFromStack = stack.Pop();
                            var copyForStack = walkerFromStack.next;
                            walkerFromStack.next = new ElementList(walker.position + 1, item, position);
                            walkerFromStack.next.next = copyForStack;
                            walkerFromStack.next.down = previousLevelItem;
                            previousLevelItem = walkerFromStack.next;
                            position++;
                        }
                        else
                        {
                            var newList = new List();
                            newList.element = new ElementList(0, MajorList.list.element.value, MajorList.list.element.level + 1);
                            newList.element.next = new ElementList(walker.position + 1, item, position);
                            newList.element.next.down = previousLevelItem;
                            newList.element.down = MajorList.list.element;
                            previousLevelItem = newList.element.next;
                            var copyMajorList = MajorList;
                            MajorList = new MainList();
                            MajorList.list = newList;
                            MajorList.nextList = copyMajorList;
                            MajorList.size = copyMajorList.size;
                            position++;
                        }
                    }
                    return;
                }
                else
                {
                    stack.Push(walker);
                    walker = walker.down;
                }
            }

            if (walker != null && walker.next != null && item.CompareTo(walker.next.value) >= 0)
            {
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && item.CompareTo(walker.next.value) < 0)
            {
                walker = walker.down;
            }
        }
    }

    private T FindElementByIndex(int index)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }

        var walker = MajorList.list.element;
        while (walker != null)
        {
            if (walker != null && walker.position == index)
            {
                return walker.value;
            }
            if (walker != null && walker.next != null && index >= walker.next.position)
            {
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && index <= walker.next.position)
            {
                walker = walker.down;
            }
        }
        throw new IncorrectIndexException();
    }

    public T this[int index] { get => FindElementByIndex(index); set => throw new NotImplementedException(); }

    public int Count => MajorList == null ? throw new NullReferenceException() : MajorList.size;

    public bool IsReadOnly => false;


    public void Clear()
    {
        MajorList = null;
    }

    public bool Contains(T item)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }

        var walker = MajorList.list.element;
        while (walker != null)
        {
            if (walker.level == 0)
            {
                while (walker.next != null && item.CompareTo(walker.next.value) >= 0)
                {
                    walker = walker.next;
                }
                return item.CompareTo(walker.value) == 0;
            }
            if (walker != null && walker.next != null && item.CompareTo(walker.next.value) >= 0)
            {
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && item.CompareTo(walker.next.value) < 0)
            {
                walker = walker.down;
            }
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }
        var walker = MajorList;
        while (walker.nextList != null)
        {
            walker = walker.nextList;
        }

        if (walker.list == null || walker.list.element == null)
        {
            throw new NullReferenceException();
        }

        var walkerForArray = walker.list.element;
        var listArray = new List<T>();

        while (walkerForArray != null)
        {
            listArray.Add(walkerForArray.value);
            walkerForArray = walkerForArray.next;
        }

        int sizeListArray = listArray.Count;
        if (arrayIndex > array.Length)
        {
            throw new IndexOutOfRangeException();
        }

        int j = 0;
        while (j < sizeListArray)
        {
            if (arrayIndex >= array.Length)
            {
                Array.Resize(ref array, array.Length + 1);
            }
                array[arrayIndex] = listArray[j];
                ++arrayIndex;
            ++j;
        }

        array = listArray.ToArray();
    }

    private class ListEnum : IEnumerator<T>
    {
        private T[] listEnum;

        int position = -1;

        public ListEnum(T[] list)
        {
            listEnum = list;
        }

        public object Current
        {
            get
            {
                try
                {
                    return listEnum[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IncorrectIndexException();
                }
            }

        }

        T IEnumerator<T>.Current
        {
            get
            {
                return (T)Current;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            position++;
            return (position < listEnum.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (MajorList == null || MajorList.list == null)
        {
            throw new NullReferenceException();
        }
        var walker = MajorList;
        while (walker.nextList != null)
        {
            walker = walker.nextList;
        }
        var arrayValue = new T[MajorList.size];

        if (walker.list == null)
        {
            throw new NullReferenceException();
        }

        var walkerList = walker.list.element;
        if (walkerList == null)
        {
            throw new NullReferenceException();
        }
        int i = 0;
        while (walkerList != null)
        {
            arrayValue[i] = walkerList.value;
            ++i;
            walkerList = walkerList.next;
        }
        return new ListEnum(arrayValue);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public int IndexOf(T item)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }

        var walker = MajorList.list.element;
        while (walker != null)
        {
            if (walker.level == 0)
            {
                return item.CompareTo(walker.value) == 0 ? walker.position : -1;
            }
            if (walker != null && walker.next != null && item.CompareTo(walker.next.value) >= 0)
            {
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && item.CompareTo(walker.next.value) < 0)
            {
                walker = walker.down;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        throw new IncorrectMethodException();
    }

    public bool Remove(T item)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }
        var walker = MajorList.list.element;
        var previousWalker = walker;
        var mainLevel = MajorList;
        while (walker != null)
        {
            if (item.CompareTo(walker.value) == 0)
            {
                var copy = walker.next;
                if (previousWalker == null)
                {
                    throw new NullReferenceException();
                }
                previousWalker.next = copy;
                walker = walker.down;
                previousWalker = walker;
                if (walker == null)
                {
                    return true;
                }
            }

            if (walker != null && walker.next != null && item.CompareTo(walker.next.value) >= 0)
            {
                previousWalker = walker;
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && item.CompareTo(walker.next.value) < 0)
            {
                previousWalker = walker;
                if (mainLevel == null)
                {
                    throw new NullReferenceException();
                }
                mainLevel = mainLevel.nextList;
                walker = walker.down;
            }
        }
        return false;
    }

    public void RemoveAt(int index)
    {
        if (MajorList == null || MajorList.list == null || MajorList.list.element == null)
        {
            throw new NullReferenceException();
        }
        var walker = MajorList.list.element;
        var previousWalker = walker;
        var mainLevel = MajorList;
        while (walker != null)
        {
            if (index == walker.position)
            {
                var copy = walker.next;
                if (previousWalker == null)
                {
                    throw new NullReferenceException();
                }
                previousWalker.next = copy;
                walker = walker.down;
                previousWalker = walker;
            }

            if (walker != null && walker.next != null && walker.next.position <= index)
            {
                previousWalker = walker;
                walker = walker.next;
            }
            else if (walker != null && walker.next != null && index < walker.next.position)
            {
                previousWalker = walker;
                if (mainLevel == null)
                {
                    throw new NullReferenceException();
                }
                mainLevel = mainLevel.nextList;
                walker = walker.down;
            }
        }
    }

    private class ElementList
    {
        public ElementList(int index, T item, int standart)
        {
            value = item;
            position = index;
            level = standart;
        }

        public ElementList? next { get; set; }

        public ElementList? down { get; set; }

        public T? value { get; set; }

        public int position { get; set; }

        public int level { get; set; }
    }
}
