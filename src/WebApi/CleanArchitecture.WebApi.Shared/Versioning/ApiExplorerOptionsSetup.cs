using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Shared.Versioning;

internal class ApiExplorerOptionsSetup : IConfigureOptions<ApiExplorerOptions>
{
    public void Configure(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    }
}
