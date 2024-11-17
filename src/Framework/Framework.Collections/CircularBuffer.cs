namespace Framework.Collections;

public class CircularBuffer<T>
{
    public enum DequeueStrategy
    {
        FIFO,
        LIFO
    }

    private readonly T[] array;
    private int count;
    private int head;
    private int tail;

    public CircularBuffer(int capacity)
    {
        if (capacity < 1)
        {
            throw new ArgumentException("CircularBuffer.Capacity must be greater than zero", nameof(capacity));
        }

        Capacity = capacity;

        array = new T[capacity];

        count = 0;
        head = tail = 0;
    }

    public readonly int Capacity;
    public int Count => count;

    public T? First
    {
        get
        {
            return count == 0 ? default : array[head];
        }
    }

    public T? Last
    {
        get
        {
            return count == 0 ? default : array[(tail + Capacity - 1) % Capacity];
        }
    }

    public IEnumerable<T> Items
    {
        get
        {
            for (int i = 0; i < count; i++)
            {
                yield return array[(head + i) % Capacity];
            }
        }
    }

    public IEnumerable<T> ReverseItems
    {
        get
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return array[(head + i) % Capacity];
            }
        }
    }

    public void Enqueue(T value)
    {
        array[tail] = value;

        if (count == Capacity)
        {
            head = tail = (head + 1) % Capacity;
        }
        else
        {
            tail = (tail + 1) % Capacity;
            count++;
        }
    }

    public T? Dequeue(DequeueStrategy strategy = DequeueStrategy.FIFO)
    {
        if (count == 0)
        {
            return default;
        }

        if (strategy == DequeueStrategy.FIFO)
        {
            var result = array[head];
            head = (head + 1) % Capacity;
            count--;
            return result;
        }
        else
        {
            tail = (tail + Capacity - 1) % Capacity;
            count--;
            return array[tail];
        }
    }
}
