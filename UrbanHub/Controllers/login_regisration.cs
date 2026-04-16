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

        public IActionResult login()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult registration()
        {
            return View("registration");
        }
    }
}
