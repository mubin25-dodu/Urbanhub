using Microsoft.AspNetCore.Mvc;

namespace UrbunHub.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View("message");
        }
    }
}