namespace AdventOfCode.Shared.Days.Six;

public class FixedQueue<T> : Queue<T>
{
    public int MaxSize { get; }

    public FixedQueue(int maxSize)
    {
        MaxSize = maxSize;
    }

    public new T? Enqueue(T item)
    {
        base.Enqueue(item);
        return Count > MaxSize ? Dequeue() : default;
    }
}
