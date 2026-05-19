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
    public class UserBookings(UrbanHubDbContext context , UserCard userCard)
    {
        public Result<List<ParkINBooking>> GetAll()
        {
            var result = new Result<List<ParkINBooking>>();
            try
            {
                var Bookings = context.ParkingBookings.Where(p => p.RenterID == userCard.UserId )
                    .Include(p => p.Parking).OrderByDescending(p => p.Date).ToList();
                if(Bookings == null || Bookings.Count == 0)
                {
                    result.Data = null;
                    result.Message = "No bookings found.";
                    result.Status = false;
                    return result;
                }
                

                result.Data = Bookings;
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
        public Result<ParkINBooking> CancelBooking( int id)
        {
            var result = new Result<ParkINBooking>();
            try
            {
                var cancel = context.ParkingBookings.Find(id);
                if(cancel == null)
                {
                    result.Data = null;
                    result.Message = "No bookings found.";
                    result.Status = false;
                    return result;
                }
                cancel.Status ="Cancelled";
                context.SaveChanges();

                result.Data = null;
                result.Message = "Booking cancelled successfully.";
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
