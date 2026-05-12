using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHub.shared;
using UrbanHubManagement.repo;

namespace UrbanHub.web.Controllers
{
    public class ParkINHome( ParkinHome repo) : Controller
    {
        [Route("ParkIN")]
        public IActionResult Browse()
        {
            var result = repo.GetAllParkingSpaces();
            return View(result.Data ?? new ParkInBrowseModel());
        }

        [HttpGet("ParkIN/Nearby")]
        public async Task<IActionResult> BrowseNearby(double lat, double lng)
        {
            var result = await repo.NearBy(5000, lat, lng);
            return View("Browse" , result.Data ?? new ParkInBrowseModel());
        }

        [HttpPost]
        public async Task<IActionResult> Search(ParkInBrowseModel data)
        {
            if (data.SearchSpaces == null)
            {
                return RedirectToAction("Browse");
            }

            var result = await repo.Search(data.SearchSpaces);
            return View("Browse", result.Data ?? new ParkInBrowseModel { SearchSpaces = data.SearchSpaces });
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
