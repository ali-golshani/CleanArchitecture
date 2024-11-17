using Microsoft.AspNetCore.OData.Query;

namespace CleanArchitecture.WebApi.Shared.OData;

public class EnableODataQueryAttribute : EnableQueryAttribute
{
    public EnableODataQueryAttribute()
    {
        AllowedQueryOptions = AllowedQueryOptions.All;
        EnsureStableOrdering = false;
        PageSize = ODataConfigs.PageSize;
        MaxExpansionDepth = ODataConfigs.MaxExpansionDepth;
        MaxTop = ODataConfigs.MaxTop;
    }
}
