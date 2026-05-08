using System.Drawing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.shared;

namespace UrbanHubManagement.repo
{
    public class ParkinHome(UrbanHubDbContext context , IMapper mapper )
    {
        public Result<List<ParkingSpaceDTO>> GetAllParkingSpaces()
        {
            var result = new Result<List<ParkingSpaceDTO>>();
            try
            {
                var parkingSpaces =  context.ParkingSpaces.Where(p=>p.IsAvailable==true).ToList();
                var newdata = mapper.Map<List<ParkingSpaceDTO>>(parkingSpaces);
                result.Data = newdata;
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
        public async Task<Result<List<ParkingSpaceDTO>>> NearBy( int distance , double lat , double lon)
        {
            var result = new Result<List<ParkingSpaceDTO>>();
            var currentLocation = new NetTopologySuite.Geometries.Point(lon ,lat  ) { SRID = 4326 };
            try
            {

                var nearby = await context.ParkingSpaces
                    .Where(c => c.Location.Distance(currentLocation) <= distance )
                    .ToListAsync();

                var filtered = nearby
                    .Where(c => c.Location != null && GetDistanceKm(lat, lon, c.Location.Y, c.Location.X) <= distance)
                    .OrderBy(c => GetDistanceKm(lat, lon, c.Location!.Y, c.Location!.X))
                    .ToList();


                var newdata = mapper.Map<List<ParkingSpaceDTO>>(filtered);

                //passing distance to DTO
                foreach (var i in newdata)
                {
                    var space = filtered.FirstOrDefault(p => p.ID == i.ID);
                    i.Distance = Math.Round(GetDistanceKm(lat, lon, space!.Location!.Y, space.Location!.X), 1);
                }
               
                result.Data = newdata;
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

        public async Task<Result<List<SearchParkingSpace>>> Search(SearchParkingSpace data)
        {
            //var result = new Result<List<SearchParkingSpace>>();
            //try
            //{

            //    var nearby = await context.ParkingSpaces
            //        .Where(c => c.VehicleType == data.Type && c.IsAvailable== true && c.Address.Contains(data.SearchText)
            //        && c.Available.Contains(data.DateAndTime))
            //        .ToListAsync();

            //    var filtered = nearby
            //        .Where(c => c.Location != null && GetDistanceKm(lat, lon, c.Location.Y, c.Location.X) <= distance)
            //        .OrderBy(c => GetDistanceKm(lat, lon, c.Location!.Y, c.Location!.X))
            //        .ToList();


            //    var newdata = mapper.Map<List<ParkingSpaceDTO>>(filtered);

            //    //passing distance to DTO
            //    foreach (var i in newdata)
            //    {
            //        var space = filtered.FirstOrDefault(p => p.ID == i.ID);
            //        i.Distance = Math.Round(GetDistanceKm(lat, lon, space!.Location!.Y, space.Location!.X), 1);
            //    }

            //    result.Data = newdata;
            //    result.Message = "Parking spaces retrieved successfully.";
            //    result.Status = true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    result.Data = null;
            //    result.Message = "An error occurred while retrieving parking spaces.";
            //    result.Status = false;
            //    throw;
            //}
            //return result;
        }

        // Haversine formula. km distance
        private double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            return R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }

    }
   
}
