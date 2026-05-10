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
    public class ParkinViewDetails(UrbanHubDbContext context , IMapper mapper , UserCard card)
    {
        public Result<ParkingDetailsModel> GetParkingSpace(int id)
        {
            var result = new Result<ParkingDetailsModel>();
            try
            {
                //getting parking infos
                var parkingSpace =  context.ParkingSpaces.Find(id);
                if (parkingSpace == null)
                {
                    result.Data = null;
                    result.Message = "Parking space not found.";
                    result.Status = false;
                    return result;
                }

                //getting booking infos

                var bookings = context.ParkingBookings.Where(e => e.ParkingID == id 
                                                                  && (e.Status == "Booked" )).ToList();
                if (bookings == null || bookings.Count == 0)
                {
                    result.Message = "No Bookings Found";

                }

                //the bug i always face ("list of" bookings and parking space)

                result.Data = new ParkingDetailsModel()
                {
                    ParkingSpaces = parkingSpace ?? new ParkingSpace(),
                    ParkingBookings = bookings ?? new List<ParkingBooking>()
                };
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

        public Result<ParkingBookingDTO> RequestBooking(ParkingBookingDTO data)
        {
            var result = new Result<ParkingBookingDTO>();

            try
            {
                //if already send a request
                var check = context.ParkingBookings.Any(e => e.ParkingID == data.ParkingID && e.RenterID == card.UserId);
                if (check)
                {
                    result.Data = null;
                    result.Message = "You have already requested a booking for this parking space. " +
                                     "Cancel it first or wait for owner's response.";
                    result.Status = false;
                    return result;
                }

                //if the time slot is already booked in backend check... 
                //infrontend checking is not enough maybe another user is looking for this same spot same time 
                // faster internet owala winns  
                var existingBookings = context.ParkingBookings
                    .Where(b => b.ParkingID == data.ParkingID
                                && b.Status == "Booked"
                                && b.StartingTime < data.EndingTime
                                && b.EndingTime > data.StartingTime)
                    .ToList();

                if (existingBookings.Count > 0)
                {
                    result.Data = data;
                    result.Status = false;
                    result.Message = "This time slot conflicts with an existing booking.";
                    return result;
                }

                TimeSpan amount = data.EndingTime-data.StartingTime;
                double hours = amount.TotalHours;

                var save = new ParkingBooking();
                save.ParkingID = data.ParkingID;
                save.OwnerID = data.OwnerID;
                save.StartingTime = data.StartingTime;
                save.EndingTime = data.EndingTime;
                save.Status = "Pending";
                save.RenterID = card.UserId;
                save.PaymentAmount= decimal.Parse((hours * (double)data.PaymentAmount).ToString());
                save.PaymentStatus = "Pending";
                save.Date = DateTime.Now;
                context.ParkingBookings.Add(save);
                context.SaveChanges();
                
                result.Data = null;
                result.Status = true;
                result.Message = "Booking Request Send Successfully";

            }
            catch (Exception e)
            {
                result.Data = data;
                result.Message = "something went wrong";
                result.Status = false;
                throw;
            }
            return result;
        }
    }
   
}
