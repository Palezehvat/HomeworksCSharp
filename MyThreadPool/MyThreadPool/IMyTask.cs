namespace MyThreadPool;

/// <summary>
/// Interface for creating tasks
/// </summary>
public interface IMyTask<TResult>
{
    public bool IsCompleted { get; }
    public TResult? Result { get; }
    /// <summary>
    /// A method for solving subtasks from the results obtained from the tasks
    /// </summary>
    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> suppiler);
}