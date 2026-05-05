using Microsoft.AspNetCore.Mvc;

namespace UrbanHub.Controllers
{
    public class ParkINHome : Controller
    {
        [Route("ParkIN")]

        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult LiveMap()
        {
            return View();
        }
    }
   
}
