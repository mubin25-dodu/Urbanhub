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
    public class ManageBookings(UrbanHubDbContext context , UserCard userCard)
    {
        public Result<List<ParkingBooking>> GetAll()
        {
            var result = new Result<List<ParkingBooking>>();
            try
            {
                var bookings = context.ParkingBookings.Where(p => p.OwnerID == userCard.UserId).
                    Include(p => p.Renter).Include(p => p.Parking)
                    .OrderByDescending(p => p.Date)
                    .ToList();
                if(bookings == null || bookings.Count == 0)
                {
                    result.Data = null;
                    result.Message = "No Parking Bookings found.";
                    result.Status = false;
                    return result;
                }
                

                result.Data = bookings ?? new List<ParkingBooking>() ;
                result.Message = "Parking Bookings retrieved successfully.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while retrieving Bookings.";
                result.Status = false;
                throw;
            }

            return result;
        }
        public Result<ParkingBooking> Accept( int id)
        {
            var result = new Result<ParkingBooking>();
            try
            {
                var spaces = context.ParkingBookings.Find(id);
                if (spaces == null)
                {
                    result.Data = null;
                    result.Message = "No Parking Space found.";
                    result.Status = false;
                    return result;
                }
                spaces.Status = "Accepted";
                
                context.ParkingBookings.Update(spaces);
                context.SaveChanges();

                result.Data = null;
                result.Message = "Parking request accepted.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while removing parking space.";
                result.Status = false;
                throw;
            }

            return result;
        }

        public Result<ParkingBooking> Cancel( int id)
        {
            var result = new Result<ParkingBooking>();
            try
            {
                var spaces = context.ParkingBookings.Find(id);
                if (spaces == null)
                {
                    result.Data = null;
                    result.Message = "No Parking Space found.";
                    result.Status = false;
                    return result;
                }
                spaces.Status = "Canceled";
                
                context.ParkingBookings.Update(spaces);
                context.SaveChanges();

                result.Data = null;
                result.Message = "Parking request Canceled.";
                result.Status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while removing parking space.";
                result.Status = false;
                throw;
            }

            return result;
        }

    }
}
