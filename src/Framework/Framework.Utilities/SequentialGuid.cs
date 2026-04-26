using SequentialGuid;

namespace Framework.Utilities;

public static class SequentialGuid
{
    public static Guid NewGuid()
    {
        return GuidV8Time.NewGuid();
    }
}
