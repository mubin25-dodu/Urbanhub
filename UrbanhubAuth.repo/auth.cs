using Microsoft.EntityFrameworkCore.Design;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHub.Models;

namespace UrbanHubManagement.repo
{
    public class Auth( UrbanHubDbContext context )
    {
        public result<List<User>> UserExist(LoginDTO data)
        {
            var result = new result<List<User>>();
            try
            {
                var check = context.Users.Where(x => x.Email == data.Email && x.Password == data.Password).FirstOrDefault();
                if (check == null)
                {
                    result.data=null;
                    result.message = "Wrong email or password";
                    result.status = false;
                }
                else
                {
                    result.data = null;
                    result.message = "User found";
                    result.status = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.data = null;
                result.message = "An error occurred while checking user existence.";
                result.status = false;
                throw;
            }
            return result;
        }

        public result<List<Registration>> register(Registration data)
        {
            var result = new result<List<Registration>>();
            try
            {
                var check = context.Users.Where(x => x.Email == data.Email).FirstOrDefault();
                var checkreg = context.Registrations.Where(x => x.Email != null && x.Email == data.Email).ToList();
                if (check != null )
                {
                    result.data = null;
                    result.message = "User already exists";
                    result.status = false;
                }
                else
                {
                    int id = new Random().Next(1, 1000000);
                    if (checkreg.Count == 0)
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

                    //mail sending
                    result.data = null;
                    result.message = "An Email hase been send to you please confirm";
                    result.status = true;
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
        

    }  
   
}
