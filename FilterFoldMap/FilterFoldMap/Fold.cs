namespace MapFilterFold;

/// <summary>
/// Implements the fold function
/// </summary>
public class FoldList
{
    /// <summary>
    /// The function accumulates the original value
    /// </summary>
    /// <typeparam name="TypeOfValue">The type that the function works with</typeparam>
    /// <param name="list">A list with the selected type</param>
    /// <param name="accumulatedValue">Cumulative value with the selected type</param>
    /// <param name="function">A function for accumulating a value, takes an accumulating value and a list item</param>
    /// <returns>Accumulated value</returns>
    public TypeOfValue Fold<TypeOfValue>(List<TypeOfValue> list, TypeOfValue accumulatedValue, Func<TypeOfValue, TypeOfValue, TypeOfValue> function)
    {
        if (list == null)
        {
            return default(TypeOfValue);
        }
        foreach (var item in list)
        {
            accumulatedValue = function(item, accumulatedValue);
        }
        return accumulatedValue;
    }
}