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
    public class MySpace(UrbanHubDbContext context , UserCard userCard)
    {
        public Result<List<ParkingSpace>> GetAll()
        {
            var result = new Result<List<ParkingSpace>>();
            try
            {
                var spaces = context.ParkingSpaces.Where(p => p.OwnerId == userCard.UserId )
                    .ToList();
                if(spaces == null || spaces.Count == 0)
                {
                    result.Data = null;
                    result.Message = "No Parking Space found.";
                    result.Status = false;
                    return result;
                }
                

                result.Data = spaces;
                result.Message = "";
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
        public Result<List<ParkingSpace>> Delete( int id)
        {
            var result = new Result<List<ParkingSpace>>();
            try
            {
                var spaces = context.ParkingSpaces.Find(id);
                if (spaces == null)
                {
                    result.Data = null;
                    result.Message = "No Parking Space found.";
                    result.Status = false;
                    return result;
                }

                context.ParkingSpaces.Remove(spaces);
                context.SaveChanges();

                result.Data = null;
                result.Message = "Parking space deleted.";
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
