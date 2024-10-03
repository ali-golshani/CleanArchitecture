namespace Framework.Core;

public interface IMaxHeap<T>
{
    int Count { get; }

    T Peek();
    T Pop();
    void Push(T item);

    void Clear();
}