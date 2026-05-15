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
    public class Notifications(UrbanHubDbContext context , UserCard userCard)
    {
        public Result<List<Notification>> GetAll()
        {
            var result = new Result<List<Notification>>();
            try
            {
                var notifications = context.Notifications.Where(p => p.To == userCard.UserId).
                    OrderByDescending(p => p.Date)
                    .ToList();
                if(notifications == null || notifications.Count == 0)
                {
                    result.Data = null;
                    result.Message = "No Notifications found.";
                    result.Status = false;
                    return result;
                }
                
                result.Data = notifications;
                result.Message = "Notifications retrieved successfully.";
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
        
    }
}
