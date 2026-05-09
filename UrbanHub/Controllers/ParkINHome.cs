using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHubManagement.repo;

namespace UrbanHub.web.Controllers
{
    public class ParkINHome( ParkinHome repo) : Controller
    {
        [Route("ParkIN")]
        public IActionResult Browse()
        {
            var result = repo.GetAllParkingSpaces();
            return View(result.Data ?? new ParkINBrowseDTO());
        }

        [HttpGet("ParkIN/Nearby")]
        public async Task<IActionResult> BrowseNearby(double lat, double lng)
        {
            var result = await repo.NearBy(5000, lat, lng);
            return View("Browse" , result.Data ?? new ParkINBrowseDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Search(ParkINBrowseDTO data)
        {
            if (data.SearchSpaces == null)
            {
                return RedirectToAction("Browse");
            }

            var result = await repo.Search(data.SearchSpaces);
            return View("Browse", result.Data ?? new ParkINBrowseDTO { SearchSpaces = data.SearchSpaces });
        }

        //public IActionResult MapView( int id)
        //{
        //    return Redirect();
        //}

        public IActionResult LiveMap()
        {
            return View();
        }

       
    }
   
}
