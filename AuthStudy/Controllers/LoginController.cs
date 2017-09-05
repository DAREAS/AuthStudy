using System.Security.Claims;
using AuthStudy.DAL.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPatch]
        public IActionResult Patch(int id, [FromBody] Delta<User> user)
        {
            var userNew = new User
            {
                Id = 1,
                Username = "dareas",
                Password = "xxxxxxxx"
            };

            user.Patch(userNew);

            return new JsonResult(userNew);
        }
    }
}
