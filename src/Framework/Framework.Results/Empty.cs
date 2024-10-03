namespace Framework.Results;

public readonly struct Empty : IEquatable<Empty>, IComparable<Empty>, IComparable
{
    public static readonly Empty Value = new Empty();
    public static readonly Task<Empty> Task = System.Threading.Tasks.Task.FromResult(Value);

    public int CompareTo(Empty other) => 0;
    int IComparable.CompareTo(object? obj) => 0;
    public override int GetHashCode() => 0;
    public bool Equals(Empty other) => true;
    public override bool Equals(object? obj) => obj is Empty;

    public static bool operator ==(Empty _, Empty __) => true;
    public static bool operator !=(Empty _, Empty __) => false;
    public static bool operator >(Empty _, Empty __) => false;
    public static bool operator <(Empty _, Empty __) => false;
    public static bool operator >=(Empty _, Empty __) => true;
    public static bool operator <=(Empty _, Empty __) => true;

    public override string ToString() => "()";
}
