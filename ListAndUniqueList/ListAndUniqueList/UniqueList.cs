namespace ListAndUniqueList;

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

    public override void AddElement(int value)
    {
        if (Contains(value))
        {
            throw new InvalidItemException();
        }
        base.AddElement(value);
    }

    public override void RemoveElement(ref int item)
    {
        base.RemoveElement(ref item);
    }

    public override void ChangeValueByPosition(int position, int newValue)
    {
        if (Contains(newValue))
        {
            throw new InvalidItemException();
        }
        base.ChangeValueByPosition(position, newValue);
    }

    public override bool IsEmpty()
    {
        return base.IsEmpty();
    }

    public override int ReturnValueByPosition(int Position)
    {
        return base.ReturnValueByPosition(Position);
    }
}
