using Microsoft.AspNetCore.OData.Query.Validator;

namespace CleanArchitecture.WebApi.Shared.OData;

public static class ODataConfigs
{
    public const int MaxTop = 1000;
    public const int PageSize = 100;
    public const int MaxExpansionDepth = 5;

    public static void SetValidationSettings(ODataValidationSettings validationSettings)
    {
        validationSettings.MaxTop = MaxTop;
        validationSettings.MaxExpansionDepth = MaxExpansionDepth;
    }
}
