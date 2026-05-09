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
    public class ParkinViewDetails(UrbanHubDbContext context , IMapper mapper )
    {
        public Result<ParkingSpace> GetParkingSpace(int id)
        {
            var result = new Result<ParkingSpace>();
            try
            {
                var parkingSpace =  context.ParkingSpaces.Find(id);
                result.Data = parkingSpace;
                result.Message = "Parking space retrieved successfully.";
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

    }
   
}
