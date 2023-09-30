namespace Lazy;

/// <summary>
/// The class implements, Gets the created object
/// </summary>
public interface ILazy<T>
{
    /// <summary>
    /// Getting the created object
    /// </summary>
    T? Get();
}