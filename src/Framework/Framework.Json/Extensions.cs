using JsonDiffPatchDotNet;

namespace Framework.Json;

public static class Extensions
{
    public static string JsonDifferences(this string first, string second)
    {
        var jdp = new JsonDiffPatch();
        var diff = jdp.Diff(first, second);
        return diff?.ToString() ?? "{ }";
    }
}