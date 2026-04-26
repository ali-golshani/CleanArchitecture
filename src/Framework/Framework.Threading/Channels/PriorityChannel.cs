using Framework.Collections;

namespace Framework.Threading.Channels;

public class PriorityChannel<T> : IChannel<T>
{
    private readonly object sync;
    private readonly MaxHeap<Wrapper> heap;
    private int orderNumber;

    public PriorityChannel(IComparer<T> comparer)
    {
        sync = new object();
        orderNumber = 0;
        heap = new MaxHeap<Wrapper>(new Comparer(comparer));
    }

    public int Count => heap.Count;

    public T Receive()
    {
        lock (sync)
        {
            while (heap.Count == 0)
                Monitor.Wait(sync);
            return heap.Pop().Value;
        }
    }

    public void ClearAndPost(T value)
    {
        lock (sync)
        {
            heap.Clear();
            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);
        }
    }

    public void Post(T value)
    {
        lock (sync)
        {
            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);
        }
    }

    public bool PostIfEmpty(T value)
    {
        lock (sync)
        {
            if (heap.Count > 0)
            {
                return false;
            }

            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);

            return true;
        }
    }

    public void Clear()
    {
        lock (sync)
        {
            heap.Clear();
        }
    }


    private sealed class Wrapper
    {
        public Wrapper(T value, int orderNumber)
        {
            Value = value;
            OrderNumber = orderNumber;
        }

        public T Value { get; }
        public int OrderNumber { get; }
    }

    private sealed class Comparer : IComparer<Wrapper>
    {
        private readonly IComparer<T> comparer;

        public Comparer(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public int Compare(Wrapper? x, Wrapper? y)
        {
            var result = comparer.Compare(x!.Value, y!.Value);

            if (result == 0)
            {
                result = y.OrderNumber.CompareTo(x.OrderNumber);
            }

            return result;
        }
    }
}
