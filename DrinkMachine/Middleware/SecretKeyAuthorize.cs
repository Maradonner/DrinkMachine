using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DrinkMachine.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SecretKeyAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var secretKeyConfig = config["SecretKey"];

        var secretQuery = context.HttpContext.Request.Query["key"].ToString();

        if (secretQuery != secretKeyConfig)
        {
            context.Result = new UnauthorizedResult();
        }

    }
}
