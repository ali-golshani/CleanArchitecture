namespace Framework.Collections;

public class MaxHeap<T>
{
    private readonly IComparer<T> comparer;
    private readonly List<T> elements;

    public MaxHeap(IComparer<T> comparer)
    {
        this.comparer = comparer;
        elements = new List<T>();
    }

    public int Count => elements.Count;

    public void Clear()
    {
        elements.Clear();
    }

    public void Push(T item)
    {
        int i = elements.Count;
        elements.Add(item);

        while (Parent(i) >= 0 && Compare(item, elements[Parent(i)]) > 0)
        {
            elements[i] = elements[Parent(i)];
            i = Parent(i);
        }

        elements[i] = item;
    }

    public T Pop()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        T item = elements[0];
        Delete(0);
        return item;
    }

    public T Peek()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return elements[0];
    }

    private void Delete(int index)
    {
        int last = elements.Count - 1;
        elements[index] = elements[last];
        elements.RemoveAt(last);
        Heapify(index);
    }

    private void Heapify(int i)
    {
        int left = LeftChild(i);
        int right = RightChild(i);
        int largest = i;

        if (left < elements.Count && Compare(elements[left], elements[i]) > 0)
        {
            largest = left;
        }

        if (right < elements.Count && Compare(elements[right], elements[largest]) > 0)
        {
            largest = right;
        }

        if (largest != i)
        {
            Exchange(i, largest);
            Heapify(largest);
        }
    }

    private static int Parent(int i)
    {
        return (i + 1) / 2 - 1;
    }

    private static int LeftChild(int i)
    {
        return 2 * i + 1;
    }

    private static int RightChild(int i)
    {
        return 2 * i + 2;
    }

    private void Exchange(int i, int j)
    {
        var temp = elements[i];
        elements[i] = elements[j];
        elements[j] = temp;
    }

    private int Compare(T left, T right)
    {
        return comparer.Compare(left, right);
    }
}
