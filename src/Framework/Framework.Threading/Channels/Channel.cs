namespace Framework.Threading.Channels;

public class Channel<T> : IChannel<T>
{
    private readonly object sync;
    private readonly Queue<T> queue;

    public Channel()
    {
        queue = new Queue<T>();
        sync = new object();
    }

    public int Count => queue.Count;

    public T Receive()
    {
        lock (sync)
        {
            while (queue.Count == 0)
                Monitor.Wait(sync);
            return queue.Dequeue();
        }
    }

    public T[] ReceiveAll()
    {
        lock (sync)
        {
            while (queue.Count == 0)
                Monitor.Wait(sync);
            var result = queue.ToArray();
            queue.Clear();
            return result;
        }
    }

    public void Post(T value)
    {
        lock (sync)
        {
            queue.Enqueue(value);
            Monitor.PulseAll(sync);
        }
    }

    public bool PostIfEmpty(T value)
    {
        lock (sync)
        {
            if (queue.Count > 0)
            {
                return false;
            }

            queue.Enqueue(value);
            Monitor.PulseAll(sync);

            return true;
        }
    }

    public void Clear()
    {
        lock (sync)
        {
            queue.Clear();
        }
    }

    public void ClearAndPost(T value)
    {
        lock (sync)
        {
            queue.Clear();
            queue.Enqueue(value);
            Monitor.PulseAll(sync);
        }
    }
}
