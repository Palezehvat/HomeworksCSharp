namespace MapFilterFold;

/// <summary>
/// Implements the filter function
/// </summary>
public class FilterList
{
    /// <summary>
    /// Filters the list with the selected type using the function
    /// </summary>
    /// <typeparam name="TypeOfValue">Selected data type</typeparam>
    /// <param name="list">A list with the selected type</param>
    /// <param name="function">The function for filtering a list item returns a boolean value if the list item matches the filter</param>
    /// <returns>New list after filter</returns>
    public List<TypeOfValue> Filter<TypeOfValue>(List<TypeOfValue> list, Func<TypeOfValue, bool> function)
    {
        if (list == null)
        {
            return null;
        }
        List<TypeOfValue> newList = new List<TypeOfValue>();
        foreach (var item in list)
        {
            if (function(item))
            {
                newList.Add(item);
            }
        }
        return newList;
    }
}