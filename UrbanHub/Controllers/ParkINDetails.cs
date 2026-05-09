using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHubManagement.repo;

namespace UrbanHub.web.Controllers
{
    public class ParkINDetails (ParkinViewDetails repo) : Controller
    {
        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var result = repo.GetParkingSpace(id);
            if (result.Status)
            {
                return View(result.Data);
            }
            return NotFound();
        }
    }
}
