using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanHubManagement.repo;
using UrbanHub.Entities;

namespace UrbanHub.web.Controllers
{
    [Authorize]
    public class ParkINMySpace (MySpace repo): Controller
    {
        public IActionResult MySpace()
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = repo.Delete(id);
            if (result.Status == false)
            {
                TempData["Error"] = true;
                TempData["Message"] = result.Message;
                return RedirectToAction("MySpace");
            }
            TempData["Error"] = false;
            TempData["Message"] = result.Message;

            return RedirectToAction("MySpace");
        }

       
    }
}

