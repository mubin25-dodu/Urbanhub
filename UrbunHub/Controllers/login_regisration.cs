using Microsoft.AspNetCore.Mvc;

namespace UrbunHub.Controllers
{
    public class login_regisration : Controller
    {
        [Route("Login")]
        public IActionResult login_reg()
        {
            return View("login_reg");
        }
    }
}
