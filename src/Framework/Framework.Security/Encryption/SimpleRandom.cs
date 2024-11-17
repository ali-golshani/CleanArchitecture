namespace Framework.Security.Encryption;

internal sealed class SimpleRandom
{
    private uint seed;

    public SimpleRandom(uint seed)
    {
        this.seed = seed;
    }

    public byte NextByte() => (byte)Next(byte.MinValue, byte.MaxValue + 1);

    public uint Next()
    {
        seed = (seed * 1103515245u + 13254u) % 2147483648u;
        return seed;
    }

    public uint Next(uint maxValue)
    {
        return Next() % maxValue;
    }

    public uint Next(uint minValue, uint maxValue)
    {
        return minValue + Next(maxValue - minValue);
    }

    public byte[] RandomSalt(int size)
    {
        var salt = new byte[size];
        for (int i = 0; i < size; i++)
        {
            salt[i] = NextByte();
        }
        return salt;
    }
}
