using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace CleanArchitecture.Querying.Odata;

internal static class ODataQueryFilters
{
    public static void QuerySetup(ODataQuerySettings querySettings)
    {
        querySettings.PageSize = 10;
    }

    public static void ValidationSetup(ODataValidationSettings validationSettings)
    {
        validationSettings.MaxTop = 1000;
        validationSettings.MaxExpansionDepth = 5;
    }

    public static void OptionsSetup(ODataMiniOptions options)
    {
        options
            .Select()
            .Filter()
            .OrderBy()
            .Expand()
            .Count()
            .SetMaxTop(1000)
            ;
    }
}