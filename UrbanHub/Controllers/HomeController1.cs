using Microsoft.AspNetCore.Mvc;

namespace UrbanHub.web.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult MyBookings()
        {
            return View();
        }
    }
}
