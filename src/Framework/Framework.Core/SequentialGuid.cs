namespace Framework.Core;

public static class SequentialGuid
{
    public static Guid NewGuid()
    {
        byte[] randomBytes = Guid.NewGuid().ToByteArray();
        byte[] timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
        Array.Copy(timestamp, 0, randomBytes, 0, timestamp.Length);
        return new Guid(randomBytes);
    }
}
