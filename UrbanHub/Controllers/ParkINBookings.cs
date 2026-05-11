using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanHubManagement.repo;

namespace UrbanHub.web.Controllers
{
    [Authorize]
    public class ParkINBookings (UserBookings repo): Controller
    {
        public IActionResult MyBookings()
        {
            var result = repo.GetAll();
            if (result.Status == false)
            {
                ViewBag.Error = true;
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }
    }
}

