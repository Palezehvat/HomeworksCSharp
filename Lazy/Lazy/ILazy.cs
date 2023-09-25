namespace Lazy;

/// <summary>
/// The class implementing deferred creation
/// </summary>
public interface ILazy<T>
{
    /// <summary>
    /// Getting the created object
    /// </summary>
    T Get();
}