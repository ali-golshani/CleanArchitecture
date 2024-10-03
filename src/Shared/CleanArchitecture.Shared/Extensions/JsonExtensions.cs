using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CleanArchitecture.Shared;

public static class JsonExtensions
{
    public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    public static string SerializeToJson(this object value)
    {
        return JsonSerializer.Serialize(
            value,
            value.GetType(),
            SerializerOptions);
    }

    public static object? DeserializeFromJson(this string value, Type type)
    {
        return JsonSerializer.Deserialize(
            value,
            type,
            SerializerOptions);
    }

    public static TValue? DeserializeFromJson<TValue>(this string value)
    {
        return JsonSerializer.Deserialize<TValue>(
            value,
            SerializerOptions);
    }

    public static object? TryDeserializeFromJson(this string value, Type type)
    {
        try
        {
            return DeserializeFromJson(value, type);
        }
        catch
        {
            return null;
        }
    }

    public static TValue? TryDeserializeFromJson<TValue>(this string value)
    {
        try
        {
            return DeserializeFromJson<TValue>(value);
        }
        catch
        {
            return default;
        }
    }

}
