namespace AdventOfCode.Challenges.Days.Year2022.Day06;

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
