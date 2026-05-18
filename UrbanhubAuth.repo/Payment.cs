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
using Microsoft.Data.SqlClient.DataClassification;

namespace UrbanHubManagement.repo
{
    public class Payment(UrbanHubDbContext context , UserCard userCard , IMapper mapper)
    {
        public Result<ParkingDetailsModel> Getbooking(int id)
        {
            var result = new Result<ParkingDetailsModel>();
            try
            {
                //getting booking infos
                var bookings = context.ParkingBookings.Include(p => p.Parking).
                        FirstOrDefault(p=>p.ID==id)
;
                if (bookings == null)
                {
                    result.Status = false;
                    result.Message = "No Bookings Found";

                }
                var Platformfee = Math.Round(context.PlatformWallets.Sum(w => w.PlatformFee)
                                             * bookings.PaymentAmount, 2);
                result.Data = new ParkingDetailsModel()
                {
                    ParkingBooking = bookings,
                    Platformfee = Platformfee,
                    TotalBill = Math.Round(bookings.PaymentAmount + Platformfee, 2)
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
        
        public Result<ParkingDetailsModel> ProcessPayment(int id)
        {
            var result = new Result<ParkingDetailsModel>();
            try
            {
                //getting booking infos
                var bookings = context.ParkingBookings.Include(p => p.Parking).
                        FirstOrDefault(p=>p.ID==id)
;
                if (bookings == null)
                {
                    result.Status = false;
                    result.Message = "No Bookings Found";

                }
                var Platformfee = Math.Round(context.PlatformWallets.Sum(w => w.PlatformFee)
                                             * bookings.PaymentAmount, 2);
                bookings.PaymentStatus = "Paid";
                bookings.TotalBill = Math.Round(bookings.PaymentAmount + Platformfee, 2);
                bookings.OTP = new Random().Next(100000, 999999);

                var notification = new Notification()
                {
                    From = "System",
                    To = bookings.RenterID ?? 0,
                    Title = "Parking Booking Accepted",
                    Message = $"Your parking booking has been accepted! Pay the rent to confirm your parking space." +
                              $"Address: {bookings.Parking.Address}. " +
                              $"Rent: {bookings.Parking.RentPerHour} BDT/hour." +
                              $"OTP: {bookings.OTP}",
                    Date = DateTime.Now

                };

                context.Notifications.Add(notification);
                context.SaveChanges();
                result.Status = true;
                result.Message = "Payment processed successfully. And Your OTP is: " 
                                 + bookings.OTP +"This will help you to enter the parking space";


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
