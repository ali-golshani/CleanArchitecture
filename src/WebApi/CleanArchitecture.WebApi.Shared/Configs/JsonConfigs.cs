using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class JsonConfigs
{
    public static void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
}
