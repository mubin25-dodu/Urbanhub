using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanHubManagement.repo;
using UrbanHub.Entities;

namespace UrbanHub.web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class ParkINManageBookings(ManageBookings repo): Controller
    {
        public IActionResult Bookings()
        {
            var result = repo.GetAll();
            if (result.Status == false)
            {
                ViewBag.Error = true;
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data ?? new List<ParkINBooking>());
        }

        [HttpGet]
        public IActionResult Accept(int id)
        {
            var result = repo.Accept(id);
            if (result.Status == false)
            {
                TempData["Error"] = true;
                TempData["Message"] = result.Message;
                return RedirectToAction("Bookings");
            }
            TempData["Error"] = false;
            TempData["Message"] = result.Message;

            return RedirectToAction("Bookings");
        }
        public IActionResult Cancel(int id)
        {
            var result = repo.Cancel(id);
            if (result.Status == false)
            {
                TempData["Error"] = true;
                TempData["Message"] = result.Message;
                return RedirectToAction("Bookings");
            }
            TempData["Error"] = false;
            TempData["Message"] = result.Message;

            return RedirectToAction("Bookings");
        }
        [HttpPost]
        public IActionResult RequestPayment(ParkINBooking data)
        { 
            var result = repo.RequestPayment(data);
            if (result.Status == false)
            {
                TempData["Error"] = true;
                TempData["Message"] = result.Message;
                return RedirectToAction("Bookings");
            }
            TempData["Error"] = false;
            TempData["Message"] = result.Message;

            return RedirectToAction("Bookings");
        }


    }
}
