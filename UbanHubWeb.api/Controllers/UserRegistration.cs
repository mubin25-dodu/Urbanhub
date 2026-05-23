using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Cms;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHubManagement.repo;

namespace UrbanHubWeb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistration (Auth repo ) : ControllerBase
    {
        [HttpPost]
        [Route("RegisterUserEmail")]
        public IActionResult RegisterEmail( Registration data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                { Error = true , errors = ModelState
                });
            }

            var result = repo.register(data);

            return Ok(new {message= result.Message ,  Error = result.Error});

        }
        [HttpGet]
        [Route("RegistrationCheckEmail")]
        public IActionResult RegistrationCheckEmail(Registration data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Error = true,
                    errors = ModelState
                });
            }

            if (data.Email == "" || data.Name == "" || data.Rid <= 0)
            {
                return BadRequest("Access denied");
            }

            var result = repo.CheckRegistrationEmail(data);
            return Ok(result);
        }
        [HttpPost]
        [Route("SaveUser")]

        public IActionResult SaveUser([FromBody]UserDTO data )
        {
            ModelState.Remove("Role");
            ModelState.Remove("ID");
            if (data.Password != data.ConfirmPassword)
            {
                ModelState.AddModelError("Password", "Passwords do not match");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Error = true,
                    errors = ModelState
                });
            }

            var result = repo.Save(data);
            return Ok(result);
        }


    }

}
