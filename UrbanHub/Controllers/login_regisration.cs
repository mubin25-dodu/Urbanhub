using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace UrbunHub.Controllers
{
    public class login_regisration : Controller
    {
        public bool sessiommeth = true; 
        public class user { 
        public string name { get; set; }
        public string email { get; set; }
        }
        static List<user> users;

        [Route("Login")]
        public IActionResult login_reg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult islogin( user data ,bool a)
        {
            
            if (ModelState.IsValid) {

                var userdata = users != null ? users.Where(e=> e.email == data.email).ToList() : null;
                var form = Request.Form;
                if (sessiommeth && userdata != null && userdata.Count == 1) {

                    //if (form["remember"] == "on") {
                    //    Response.Cookies.Append("email", data.email, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                    //    Response.Cookies.Append("name", data.name, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                    //}
                    //else {
                        Response.Cookies.Append("email", data.email);
                        Response.Cookies.Append("name", data.name);
                    //}
                    return Content($"Welcome {data.name}");
                }
                else {
                    return Content("user not found");
                     }
            }
            return Content("error");
        }
        [HttpPost]
        public IActionResult registration( user data)
        {
            if (ModelState.IsValid) {
                    if (users == null)
                    {
                        users = new List<user>();
                    ViewBag.success=true;
                    }
                    users.Add(data);
                    return RedirectToAction("login_reg");
            }
            return Content("something went wrong");
        }
    }
}
