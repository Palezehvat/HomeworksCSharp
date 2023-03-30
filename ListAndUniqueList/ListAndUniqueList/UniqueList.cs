namespace ListAndUniqueList;

// The same list only without repetitions
public class UniqueList : List
{
    private bool Contains(int value)
    {
        var walker = Head;
        while(walker != null)
        {
            if (walker.Value == value)
            {
                return true;
            }
            walker = walker.Next;
        }
        return false;
    }

    // Adding element to unique list
    public override void AddElement(int value)
    {
        if (Contains(value))
        {
            throw new InvalidItemException();
        }
        base.AddElement(value);
    }

    // Deleting element in unique list
    public override void RemoveElement(ref int item)
    {
        base.RemoveElement(ref item);
    }

    // Change value by position
    public override void ChangeValueByPosition(int position, int newValue)
    {
        if (Contains(newValue))
        {
            throw new InvalidItemException();
        }
        base.ChangeValueByPosition(position, newValue);
    }

    // Checks if the list is unique empty
    public override bool IsEmpty()
    {
        return base.IsEmpty();
    }

    // Returns the value by position 
    public override int ReturnValueByPosition(int Position)
    {
        return base.ReturnValueByPosition(Position);
    }
}
