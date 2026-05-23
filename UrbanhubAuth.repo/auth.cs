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
                var check = context.Users.FirstOrDefault(x => x.Email == data.Email);
                if (check == null)
                {
                    result.Data = null;
                    result.Message = "No User Found";
                    result.Error = false;
                }
                else if (check != null && check.Email!=data.Email)
                {
                    result.Data = null;
                    result.Message = "Wrong email Try again";
                    result.Error = false;
                }
                else if (check != null && check.Password != data.Password)
                {
                    result.Data = null;
                    result.Message = "Wrong password Try again";
                    result.Error = false;
                }
                else if(check.Status.ToLower() =="banned"){
                    result.Data = null;
                    result.Message = "The User Is Banned";
                    result.Error = false;
                }
                else
                {
                    result.Data = check;
                    result.Message = "User found";
                    result.Error = true;
                }
            }
            catch (Exception e)
            {
                result.Data = null;
                result.Message = e.ToString();
                result.Error = false;
                throw;
            }

            return result;
        }

        public Result<List<Registration>> register(Registration data)
        {
            var result = new Result<List<Registration>>();
            try
            {
                var check = context.Users.FirstOrDefault(x => x.Email == data.Email);
                if (check != null)
                {
                    result.Data = null;
                    result.Message = "User already exists";
                    result.Error = true;
                }
                else
                {
                    var checkreg = context.Registrations.FirstOrDefault(x => x.Email != null && x.Email == data.Email);

                    int id = new Random().Next(1, 100000);
                    if (checkreg == null)
                    {
                        var newdata = new Registration()
                        {
                            Email = data.Email,
                            Name = data.Name,
                            Rid = id
                        };
                        context.Registrations.Add(newdata);
                    }
                    else
                    {
                        checkreg.Rid = id;
                        context.Registrations.Update(checkreg);

                    }

                    context.SaveChanges();
                    //mail sending
                    result.Data = null;
                    result.Message = "An Email hase been send to you please confirm";
                    result.Error = false;
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

        public Result<Registration> CheckRegistrationEmail(Registration data )
        {
            var result = new Result<Registration>();
            var check = context.Registrations.Where(u => u.Email == data.Email && u.Rid == data.Rid);

            if (!check.Any())
            {
                result.Message = "No User Found";
                result.Error = true;
                return result ;
            }

            result.Error = false;
            return result;


        }

        public Result<UserDTO> Save(UserDTO data)
        {
            var result = new Result<UserDTO>();
            try
            {
                var check = context.Registrations.Where(u => u.Email == data.Email);
                var usercheck = context.Users.FirstOrDefault(e => e.Email == data.Email);
                
                if (usercheck != null)
                {
                    if (usercheck.Email == data.Email)
                    {
                        result.Data = data;
                        result.Error = true;
                        result.Message = "Email already Registered";
                    }
                    else if (usercheck.Phone == data.Phone)
                    {
                        result.Data = data;
                        result.Error = true;
                        result.Message = "Phone Number already Registered";
                    }
                }
                else if (check.Count() != 0 && usercheck == null)
                {
                    data.JoinDate = DateTime.Now;
                    context.Users.Add(new User()
                    {
                        Name = data.Name,
                        Email = data.Email,
                        Password = data.Password,
                        Address = data.Address,
                        Role = "User",
                        Status = "Active",
                        JoinDate = DateTime.Now,
                        Phone = data.Phone
                    });
                    context.Registrations.Remove(check.First());
                    context.SaveChanges();

                    result.Data = null;
                    result.Error = false;
                    result.Message = "Registration Successful";
                }
                return result;
            }
            catch (Exception e)
            {
                result.Data = data;
                result.Error = true;
                result.Message = e.ToString();
                throw;
            }

        }
    }

}
