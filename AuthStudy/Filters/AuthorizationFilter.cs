using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthStudy.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
       public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.User.IsInRole(context.ActionDescriptor.Id);
            }
        }
    }
}
