using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;
using UrbanHub.custom_services;
using UrbanHub.Data;
using UrbanHub.Data.Entities;
using UrbanHub.DTO;
using UrbanHub.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UrbanHub.Controllers
{
    public class login_regisration : Controller
    {
        private readonly UrbanhubDbContext _context;
        private IMapper _mapper;
        private static int rid;
        public login_regisration(UrbanhubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("Login")]
        public IActionResult login_reg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult islogin(login_reg data)
        {
            ModelState.Remove("Registration");
            if (!ModelState.IsValid)
            {
                return View("login_reg");
            }
            var check = _context.Registrations.Where(u => u.Email == data.Login.Email && u.Name == data.Login.Password);
            if (check.Count() == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Login.Email", "Email or password is incorrect.");
                return View("login_reg");
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> login_reg(login_reg data)
        {

            ModelState.Remove("Login");
            if (!ModelState.IsValid)
            {
                return View();
            }
            var check = _context.Registrations.Where(u => u.Email == data.Registration.Email);
            if (check.Count() > 0)
            {
                //ill add send email to user here
                rid = check.First().Rid;
                await sendmail(data.Registration.Email, data.Registration.Name);
                ModelState.AddModelError("Registration.Email", "Email already exists. and a mail is sent to your email.");
                return View();
            }
            else
            {
                Random rnd = new Random();
                rid = rnd.Next(1000, 9999);
                var registration = _mapper.Map<Registration>(data.Registration);
                registration.Rid = rid;
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                await sendmail(data.Registration.Email, data.Registration.Name);
            }

            return View();
        }

        //registration page actions

        [Route("Registration")]
        [HttpGet("Registration/{email}/{id}\"")]
        public IActionResult Reg_users(string email, int id)
        {
            if (email == null || id == 0)
            {
                return RedirectToAction("login_reg");
            }
            var check = _context.Registrations.Where(u => u.Email == email && u.Rid == id);

            if (check.Count() == 0)
            {
                return RedirectToAction("login_reg");
            }

            ViewBag.Email = email;
            ViewBag.Name = check.First().Name;

            return View();
        }

        [HttpPost("Registration")]
        public IActionResult Reg_users(UserDTO data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var check = _context.Registrations.Where(u => u.Email == data.Email);
            if (check.Count() != 0)
            {
                data.JoinDate = DateTime.Now;
                var adduser = _mapper.Map<User>(data);
                _context.Users.Add(adduser);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Email", "No email found on that name please go to signin");
            }
            return View();
        }
        private async Task sendmail(string e, string n)
        {
            //sending mail
            var email = e;
            var subject = "Registration Confirmation";
            var message = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Registration Confirmation</title>
            </head>
            <body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333;"">
                <div style=""max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;"">
                    <h2 style=""color: #007bff;"">Welcome to UrbanHub!</h2>
                    <p>Dear {n},</p>
                    <p>Thank you for registering with us! We're excited to have you as part of our community.</p>
                    <p>Please click the button below to complete your registration and activate your account:</p>
                    <p style=""text-align: center;"">
                        <a href='https://localhost:7019/registration?email={email}&id={rid}' style=""background-color: #007bff; color: #ffffff; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;"">Complete Registration</a>
                    </p>
                    <p>If you're having trouble with the button, you can also copy and paste the following link into your browser:</p>
                    <p><a href='https://localhost:7019/registration?email={email}&id={rid}'>https://localhost:7019/registration?email={e}&id={rid}</a></p>
                    <p>If you did not request this registration, please ignore this email.</p>
                    <hr style=""border: 0; border-top: 1px solid #eee;"">
                    <p style=""font-size: 0.9em; color: #777;"">Best regards,<br>The UrbanHub Team</p>
                </div>
            </body>
            </html>";
            // Call the email sending method (you need to implement this)
            await new send_email().SendEmail(email, subject, message);
        }
    }
}
