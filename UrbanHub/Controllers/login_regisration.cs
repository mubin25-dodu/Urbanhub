using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;
using UrbanHub.custom_services;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHub.Models;
using UrbanHubManagement.repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UrbanHub.Controllers;

public class login_regisration(Auth repo , UrbanHubDbContext context) : Controller
{

    

    [Route("Login")]
    public IActionResult login_reg()
    {
        return View();
    }
    [HttpPost]
    [Route("api/islogin")]
    public IActionResult islogin([FromBody] LoginDTO data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { status = false, errors = ModelState });
        }

        var newdata = repo.UserExist(data);
        return Ok(newdata);
    }


    [HttpPost]
    [Route("api/Reg")]
    public IActionResult login_reg([FromBody] Registration data)
    {
        ModelState.Remove("Login");
        if (!ModelState.IsValid)
        {
            return Ok(new { HasError = false, errors = ModelState });
        }
        var newdata = repo.register(data);
        int rid;
        if(newdata.status)
        {
            rid = int.Parse(newdata.AdditionalMessage);
            sendmail(data.Email, data.Name,rid);
        }
        return Ok(newdata);
       
    }

    //registration page actions

    [Route("Registration")]
    [HttpGet("Registration/{email}/{id}")]
    public IActionResult Reg_users(string email, int id)
    {
        if (email == null || id == 0)
        {
            return RedirectToAction("login_reg");
        }
        var check = context.Registrations.Where(u => u.Email == email && u.Rid == id);

        if (check.Any())
        {
            return RedirectToAction("login_reg");
        }
        HttpContext.Session.SetString("email", email);
        HttpContext.Session.SetString("name", check.First().Name);


        return View();
    }

    [HttpPost("Registration")]
    public IActionResult Reg_users(User data , string cpass)
    {
        ModelState.Remove("Role");
        if (!ModelState.IsValid)
        {
            return View();
        }
        else if(data.Password!=cpass)
        {
            ModelState.AddModelError("Password", "Passwords do not match");
            return View();
        }
        else
        {
            var check = context.Registrations.Where(u => u.Email == data.Email);
            if (check.Count() != 0)
            {
                data.JoinDate = DateTime.Now;
                context.Users.Add(data);
                context.Registrations.Remove(check.First());
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Email", "No email found on that name please go to signin");
            }
        }

        return View();
    }

    private async Task sendmail(string e, string n ,int rid)
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
                <p><a href='https://localhost:7019/registration?email={email}&id={rid}'>https://localhost:7019/registration?email={email}&id={rid}</a></p>
                <p>If you did not request this registration, please ignore this email.</p>
                <hr style=""border: 0; border-top: 1px solid #eee;"">
                <p style=""font-size: 0.9em; color: #777;"">Best regards,<br>The UrbanHub Team</p>
            </div>
        </body>
        </html>";

        // Calliing the email sending method
        await new send_email().SendEmail(email, subject, message);
    }
}