using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class AuthorizeRoleAttribute(params string[] roles) : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles = roles;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    
        var hasRole = _roles.Any(role => user.IsInRole(role));
        if (!hasRole)
        {
            context.Result = new ForbidResult();
        }
    }
}