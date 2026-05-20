using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanHub.Data;

namespace UrbanHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly UrbanHubDbContext context;

        public ParkingSpaceController(
            UrbanHubDbContext _context)
        {
            context = _context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(int id)
        {
            var result =
                await context.ParkingSpaces
                .AsNoTracking()
                .Select(x => new
                {
                    x.ID,

                    x.Address,

                    Location = new
                    {
                        Latitude =
                            x.Location != null
                            ? x.Location.Y
                            : 0,

                        Longitude =
                            x.Location != null
                            ? x.Location.X
                            : 0
                    },

                    x.RentPerHour,
                    x.Available,
                    x.IsAvailable,
                    x.Image,
                    x.Description,
                    x.OwnerId,
                    x.VehicleType
                })
                .FirstOrDefaultAsync(
                    x => x.ID == id);

            if (result == null)
            {
                return NotFound(
                    "Invalid Id");
            }

            return Ok(result);
        }
    }
}