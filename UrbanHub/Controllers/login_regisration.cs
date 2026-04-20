using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UrbanHub.Data;
using UrbanHub.Data.Entities;
using UrbanHub.Models;

namespace UrbanHub.Controllers
{
    public class login_regisration : Controller
    {
        private readonly UrbanhubDbContext _context;
        private IMapper _mapper;
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

        public IActionResult islogin()
        {
            return RedirectToAction("Index", "Home");
        }



        [HttpPost("login")]
        public IActionResult login_reg(login_reg data , string btn)
        {
            if (btn == "reg_btn" )
            { 
                ModelState.Remove("User");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var check = _context.Registrations.Where(u => u.Email == data.Registration.Email);
                if (check.Count() > 0)
                {
                    //ill add send email to user here
                    ModelState.AddModelError("Registration.Email", "Email already exists. and a mail is sent to your email.");
                    return View();
                }
                else
                {
                    var registration = _mapper.Map<Registration>(data.Registration);
                    _context.Registrations.Add(registration);
                    //ill add send email to user here
                    _context.SaveChanges();
                }
            }else if (btn == "login")
            {
                ModelState.Remove("Registration");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var check = _context.Registrations.Where(u => u.Email == data.User.Email && u.Name == data.User.Password);
                check.Count();
                if (check.Count() == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("User.Email", "Email or password is incorrect.");
                    return View();
                }
            }
            return View();
        }
    }
}
