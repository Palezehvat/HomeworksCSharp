namespace MyThreadPool;

/// <summary>
/// Interface for creating tasks
/// </summary>
public interface IMyTask<TResult>
{
    /// <summary>
    /// Check is completed Task and return result
    /// </summary>
    public bool IsCompleted { get; }
    /// <summary>
    /// Return Result Task
    /// </summary>
    public TResult? Result { get; }
    /// <summary>
    /// A method for solving subtasks from the results obtained from the tasks
    /// </summary>
    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> supplier);
}