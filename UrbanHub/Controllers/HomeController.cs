using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Models;

namespace UrbanHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrbanhubDbContext _context;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, UrbanhubDbContext context , IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //var data = _context.Registrations.ToList();
            //var map = _mapper.Map<List<RegistrationDTO>>(data);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
