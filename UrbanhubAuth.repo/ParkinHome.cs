using System.Drawing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHub.shared;
using System.Text.Json;

namespace UrbanHubManagement.repo
{
    public class ParkinHome(UrbanHubDbContext context , IMapper mapper )
    {
        public Result<ParkINBrowseDTO> GetAllParkingSpaces()
        {
            var result = new Result<ParkINBrowseDTO>();
            try
            {
                var parkingSpaces =  context.ParkingSpaces.Where(p=>p.IsAvailable==true).ToList();
                var mappedSpaces = mapper.Map<List<ParkingSpaceDTO>>(parkingSpaces);
                result.Data = new ParkINBrowseDTO
                {
                    ParkingSpaces = mappedSpaces,
                    SearchSpaces = new SearchParkingSpace
                    {
                        DateAndTime = DateTime.Now,
                        SearchText = string.Empty,
                        Type = string.Empty
                    }
                };
                result.Message = "Parking spaces retrieved successfully.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while retrieving parking spaces.";
                result.Status = false;
                throw;
            }
            return result;
        }
        public async Task<Result<ParkINBrowseDTO>> NearBy( int distance , double lat , double lon)
        {
            var result = new Result<ParkINBrowseDTO>();
            var currentLocation = new NetTopologySuite.Geometries.Point(lon ,lat  ) { SRID = 4326 };
            try
            {

                var nearby = await context.ParkingSpaces
                    .Where(c => c.Location.Distance(currentLocation) <= distance )
                    .ToListAsync();

                var filtered = nearby
                    .Where(c => c.Location != null && new HeversineFormula().GetDistanceKm(lat, lon, c.Location.Y, c.Location.X) <= distance)
                    .OrderBy(c => new HeversineFormula().GetDistanceKm(lat, lon, c.Location!.Y, c.Location!.X))
                    .ToList();


                var newdata = mapper.Map<List<ParkingSpaceDTO>>(filtered);

                //passing distance to DTO
                foreach (var i in newdata)
                {
                    var space = filtered.FirstOrDefault(p => p.ID == i.ID);
                    i.Distance = Math.Round(new HeversineFormula().GetDistanceKm(lat, lon, space!.Location!.Y, space.Location!.X), 1);
                }

                result.Data = new ParkINBrowseDTO
                {
                    ParkingSpaces = newdata,
                    SearchSpaces = new SearchParkingSpace
                    {
                        DateAndTime = DateTime.Now,
                        SearchText = string.Empty,
                        Type = string.Empty
                    }
                };
                result.Message = "Parking spaces retrieved successfully.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while retrieving parking spaces.";
                result.Status = false;
                throw;
            }
            return result;
        }

        public async Task<Result<ParkINBrowseDTO>> Search(SearchParkingSpace data)
        {
            var result = new Result<ParkINBrowseDTO>();
            try
            {
                var nearby = await context.ParkingSpaces
                    .Where(c => c.VehicleType == data.Type && c.IsAvailable == true && c.Address.Contains(data.SearchText))
                    .ToListAsync();
                
                //day 4 of trying

                // okay so lets catch the time and then lets try to filter out the extended data by matching the times and the day
                var targetDay = data.DateAndTime.DayOfWeek;
                //found the bug heheheeey..... it was comparing the time with
                //the date and time so i just need to extract the time
                //from the date and time and then compare it with the start and end time in the schedule
                var targetTime = TimeOnly.FromDateTime(data.DateAndTime);
                var filteredSpaces = nearby
                    .Where(p =>
                    {
                        //
                        var schedules = JsonSerializer.Deserialize<List<AvailabeSchadule>>(p.Available);

                        // validating day and time 
                        return schedules.Any(e =>
                            e.Day == targetDay.ToString() &&
                            TimeOnly.Parse(e.StartTime) <= targetTime &&
                            TimeOnly.Parse(e.EndTime) >= targetTime);
                    })
                    .ToList();

                var mappedSpaces = mapper.Map<List<ParkingSpaceDTO>>(filteredSpaces);
                result.Data = new ParkINBrowseDTO
                {
                    ParkingSpaces = mappedSpaces,
                    SearchSpaces = data
                };
                result.Message = "Parking spaces retrieved successfully.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while retrieving parking spaces.";
                result.Status = false;
                throw;
            }
            return result;
        }

        // Haversine formula. km distance
        

    }
   
}
