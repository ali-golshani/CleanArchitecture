using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace CleanArchitecture.WebApi.Shared.OData;

public class CustomODataQueryValidator : ODataQueryValidator
{
    public override void Validate(ODataQueryOptions options, ODataValidationSettings validationSettings)
    {
        ODataConfigs.SetValidationSettings(validationSettings);
        base.Validate(options, validationSettings);
    }
}
