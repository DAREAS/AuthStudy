using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthStudy.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logar(string userName, string password)
        {
            var user = userName;
            var pass = password;

            HttpContext.User = new ClaimsPrincipal (new ClaimsIdentity(new[]
            {
              new Claim(ClaimTypes.Role,"haushahsiuhauihsihaushaiuhsiuhauishuaih")  
            }));

            return RedirectToAction("Index", "Home");
        }
    }
}
