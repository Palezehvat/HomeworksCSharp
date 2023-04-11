namespace MapFilterFold;

/// <summary>
/// Implements the map function
/// </summary>
public class MapList
{
    /// <summary>
    /// The map changes the value of the list with the selected type according to the function
    /// </summary>
    /// <typeparam name="TypeOfValue">Selected data type</typeparam>
    /// <param name="list">A list with the selected data type</param>
    /// <param name="function">Function for conversion</param>
    /// <returns>A new list obtained using the function and the original list</returns>
    public List<TypeOfValue> Map<TypeOfValue>(List<TypeOfValue> list, Func<TypeOfValue, TypeOfValue> function)
    {
        if (list == null)
        {
            return null;
        }
        List<TypeOfValue> newList = new List<TypeOfValue>();
        foreach (var item in list)
        {
            newList.Add(function(item));
        }
        return newList;
    }
}