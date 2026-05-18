using Microsoft.AspNetCore.Mvc;
using OpenME.WEB.API.Models.Request;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(
            [FromBody] CreateUserRequest request
        )
        {
            return Ok(new CreateUserResponse());
        }
    }
}