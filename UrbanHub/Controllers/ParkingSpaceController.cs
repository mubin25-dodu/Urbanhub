using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using UrbanHub.Data;
using UrbanHub.Entities;
using UrbanHub.ViewModels;

namespace UrbanHub.web.Controllers
{
    public class ParkingSpaceController : Controller
    {
        private readonly UrbanHubDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ParkingSpaceController(
            UrbanHubDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View("ParkingSpace");
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParkingSpaceCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("ParkingSpace", vm);
            }

            string imagePath = await SaveImage(vm.ImageFile);

            var point = new Point(vm.Longitude, vm.Latitude)
            {
                SRID = 4326
            };

            var parking = new ParkingSpace
            {
                Address = vm.Address,
                Location = point,
                RentPerHour = vm.RentPerHour,
                VehicleType = vm.VehicleType,
                Available = vm.Available,
                IsAvailable = vm.IsAvailable,
                Description = vm.Description,
                Image = imagePath,

                // temporary static owner
                OwnerId = 1
            };

            _context.ParkingSpaces.Add(parking);

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Parking Space Added Successfully";

            return RedirectToAction("Create");
        }

        // SAVE IMAGE
        private async Task<string> SaveImage(IFormFile file)
        {
            string folder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "parking"
            );

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(file.FileName);

            string filePath =
                Path.Combine(folder, fileName);

            using (var stream =
                   new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/parking/" + fileName;
        }
    }
}