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
        public async Task<IActionResult> Browse()
        {
            var result = repo.GetAllParkingSpaces();
            return View(CreateBrowseModel(result.Data));
        }

        [HttpGet("ParkIN/Nearby")]
        public async Task<IActionResult> BrowseNearby(double lat, double lng)
        {
            var result = await repo.NearBy(5000, lat, lng);
            return View("Browse" , CreateBrowseModel(result.Data));
        }

        [HttpPost]
        public IActionResult Search(ParkINBrowseDTO data)
        {
            
            return RedirectToAction("Browse");
        }

        //public IActionResult MapView( int id)
        //{
        //    return Redirect();
        //}

        public IActionResult LiveMap()
        {
            return View();
        }

        private static ParkINBrowseDTO CreateBrowseModel(List<ParkingSpaceDTO>? parkingSpaces)
        {
            return new ParkINBrowseDTO
            {
                SearchSpaces = new SearchParkingSpace(),
                ParkingSpaces = parkingSpaces ?? new List<ParkingSpaceDTO>()
            };
        }
    }
   
}
