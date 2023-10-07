namespace Lazy;

/// <summary>
/// The class implements an object whose function is called only once.
/// </summary>
public interface ILazy<T>
{
    /// <summary>
    /// Gets the created object.
    /// </summary>
    T? Get();
}