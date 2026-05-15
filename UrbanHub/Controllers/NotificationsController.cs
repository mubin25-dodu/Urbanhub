using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanHubManagement.repo;

namespace UrbanHub.web.Controllers
{
    [Authorize]
    public class NotificationsController( Notifications repo) : Controller
    {
        [Route("api/Notification")]
        public IActionResult Notification()
        {
            var result = repo.GetAll();
            return Json(result);
        }
    }
}
