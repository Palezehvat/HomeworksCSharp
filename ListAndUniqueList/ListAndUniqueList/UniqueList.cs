namespace ListAndUniqueList;

/// <summary>
/// List only without repetitions
/// </summary>
public class UniqueList : List
{
    public override void AddElement(int position, int value)
    {
        if (!Contains(value))
        {
            base.AddElement(position, value);
        }
    }

    public override void ChangeValueByPosition(int position, int newValue)
    {
        if (!Contains(newValue))
        {
            base.ChangeValueByPosition(position, newValue);
        }
    }
}
