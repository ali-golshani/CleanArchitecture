namespace Framework.Threading.Channels;

public interface IChannel<T>
{
    int Count { get; }

    void Post(T value);
    bool PostIfEmpty(T value);
    void Clear();
    void ClearAndPost(T value);
    T Receive();
}
