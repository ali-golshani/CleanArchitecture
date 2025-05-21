using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.WebApi.Shared.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(ms => ms.Value?.Errors.Any() == true)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value!.Errors.Select(e => e.ErrorMessage)
                });

            var logger = context.HttpContext.RequestServices.GetService<ILogger<ValidateModelStateAttribute>>();
            logger?.LogCritical("ModelState Errors {@Errors} {@RequestPath}", errors, context.HttpContext.Request.Path);
        }
    }
}
