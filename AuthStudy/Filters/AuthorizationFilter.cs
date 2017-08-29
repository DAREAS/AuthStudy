using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthStudy.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
       public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ctx = context;
        }
    }
}
