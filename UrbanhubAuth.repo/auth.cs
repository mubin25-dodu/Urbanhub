using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Design;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UrbanHubManagement.repo
{
    public class Auth(UrbanHubDbContext context)
    {
        public Result<User> UserExist(LoginDTO data)
        {
            var result = new Result<User>();
            try
            {
                var check = context.Users.FirstOrDefault(x => x.Email.ToLower() == data.Email.ToLower());
                if (check == null)
                {
                    result.Data = null;
                    result.Message = "NO User Found";
                    result.Status = false;
                }
                else if (check != null && check.Email!=data.Email)
                {
                    result.Data = null;
                    result.Message = "Wrong email Try again";
                    result.Status = false;
                }
                else if (check != null && check.Password != data.Password)
                {
                    result.Data = null;
                    result.Message = "Wrong password Try again";
                    result.Status = false;
                }
                else if(check.Status.ToLower() =="banned"){
                    result.Data = null;
                    result.Message = "The User Is Banned";
                    result.Status = false;
                }
                else
                {
                    result.Data = check;
                    result.Message = "User found";
                    result.Status = true;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                result.Data = null;
                result.Message = "An error occurred while checking user existence.";
                result.Status = false;
                throw;
            }

            return result;
        }

        public Result<List<Registration>> register(Registration data)
        {
            var result = new Result<List<Registration>>();
            try
            {
                var check = context.Users.FirstOrDefault(x => x.Email.ToLower() == data.Email.ToLower());
                var checkreg = context.Registrations.FirstOrDefault(x => x.Email != null && x.Email.ToLower() == data.Email.ToLower());
                if (check != null)
                {
                    result.Data = null;
                    result.Message = "User already exists";
                    result.Status = false;
                }
                else
                {
                    int id = new Random().Next(1, 1000000);
                    if (checkreg == null)
                    {
                        var newdata = new Registration()
                        {
                            Email = data.Email,
                            Name = data.Name,
                            Rid = id
                        };
                        context.Registrations.Add(newdata);
                        context.SaveChanges();
                    }
                    else
                    {
                        checkreg.Rid = id;
                        context.SaveChanges();

                    }

                    //mail sending
                    result.Data = null;
                    result.Message = "An Email hase been send to you please confirm";
                    result.Status = true;
                    result.AdditionalMessage = id.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        public Result<User> Save(User data)
        {
            var result = new Result<User>();
            try
            {
                var check = context.Registrations.Where(u => u.Email.ToLower() == data.Email.ToLower());
                var usercheck = context.Users.FirstOrDefault(e => e.Email.ToLower() == data.Email.ToLower());
                if (check.Count() != 0 && usercheck == null)
                {
                    data.JoinDate = DateTime.Now;
                    context.Users.Add(data);
                    context.Registrations.Remove(check.First());
                    context.SaveChanges();

                    result.Data = null;
                    result.Status = true;
                    result.Message = "Registration Successful";
                }
                else if (usercheck != null)
                {
                    if (usercheck.Email.ToLower() == data.Email.ToLower())
                    {
                        result.Data = data;
                        result.Status = false;
                        result.Message = "Email already Registered";
                    }
                    else if (usercheck.Phone == data.Phone)
                    {
                        result.Data = data;
                        result.Status = false;
                        result.Message = "Phone Number already Registered";
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                result.Data = data;
                result.Status = false;
                result.Message = e.ToString();
                throw;
            }

        }
    }

}
