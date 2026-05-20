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
    public class AdminUserManagementCopy(UrbanHubDbContext context )
    {
        public async Task<Result<List<User>>> Get(string searchTerm)
        {
            var result = new Result<List<User>>();
            try
            {
                var getusers = new List<User>();
               
                getusers = await context.Users.Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm)).ToListAsync();
               
                if (getusers == null)
                {
                    result.Status = false;
                    result.Message = "No Users Found";
                    return result;
                }
                result.Data = getusers;
                result.Status = true;
                result.Message = $"Total {getusers.Count} Users retrieved successfully.";

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
        public async Task<Result<User>> BanUnbanUser(int id)
        {
            var result = new Result<User>();
            try
            {
                var getusers = await context.Users.FindAsync(id);
                if (getusers == null)
                {
                    result.Status = false;
                    result.Message = "No Users Found";
                    return result;
                }

                var logupdate = new Log();
                if (getusers.Status.ToLower() == "banned")
                {
                    getusers.Status = "Active";
                    result.Message = $"User Unbanned successfully.";

                }
                else
                {
                    getusers.Status = "Banned";
                    result.Message = $"User banned successfully.";
                }

                context.Users.Update(getusers);
                await context.SaveChangesAsync();

                result.Data = getusers;
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
