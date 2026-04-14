using Microsoft.AspNetCore.Mvc;


    public class UserController : Controller
    {
        //User/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    }