namespace MapFilterFold;

/// <summary>
/// Class for filter, fold, map with generics
/// </summary>
public static class FilterFoldMap
{
    /// <summary>
    /// The function accumulates the original value
    /// </summary>
    /// <typeparam name="TypeOfValue">The type that the function works with</typeparam>
    /// <param name="list">A list with the selected type</param>
    /// <param name="accumulatedValue">Cumulative value with the selected type</param>
    /// <param name="function">A function for accumulating a value, takes an accumulating value and a list item</param>
    /// <returns>Accumulated value</returns>
    public static TypeForResult Fold<TypeForResult, TypeOfValue>(
        List<TypeOfValue>? list, 
        TypeForResult accumulatedValue, 
        Func<TypeOfValue, TypeForResult, TypeForResult> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        foreach (var item in list)
        {
            accumulatedValue = function(item, accumulatedValue);
        }
        return accumulatedValue;
    }

    /// <summary>
    /// Filters the list with the selected type using the function
    /// </summary>
    /// <typeparam name="TypeOfValue">Selected data type</typeparam>
    /// <param name="list">A list with the selected type</param>
    /// <param name="function">The function for filtering a list item returns a boolean value if the list item matches the filter</param>
    /// <returns>New list after filter</returns>
    public static List<TypeOfValue> Filter<TypeOfValue>(List<TypeOfValue>? list, Func<TypeOfValue, bool> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        List<TypeOfValue> newList = new();
        foreach (var item in list)
        {
            if (function(item))
            {
                newList.Add(item);
            }
        }
        return newList;
    }

    /// <summary>
    /// The map changes the value of the list with the selected type according to the function
    /// </summary>
    /// <typeparam name="TypeOfValue">Selected data type</typeparam>
    /// <param name="list">A list with the selected data type</param>
    /// <param name="function">Function for conversion</param>
    /// <returns>A new list obtained using the function and the original list</returns>
    public static List<TypeForResult> Map<TypeForResult, TypeOfValue>(List<TypeOfValue>? list, Func<TypeOfValue, TypeForResult> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        List<TypeForResult> newList = new();
        foreach (var item in list)
        {
            newList.Add(function(item));
        }
        return newList;
    }
}