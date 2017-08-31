using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace AuthStudy.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
       public void OnAuthorization(AuthorizationFilterContext context)
       {
           string[] primeiroAcesso = {"Logar", "Index"}; 

            if (!primeiroAcesso.Contains(context.ActionDescriptor.RouteValues["action"]) && context.ActionDescriptor.RouteValues["controller"] != "Login") 
            {
                if(!context.HttpContext.User.Identity.IsAuthenticated)
                    new RedirectToPageResult("Home");
            }
        }
    }
}
