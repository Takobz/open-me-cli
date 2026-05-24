using Microsoft.AspNetCore.Mvc;
using OpenME.Core.Application.Ports.In;
using OpenME.WEB.API.Models;
using OpenME.WEB.API.Models.Request;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController(
            ICreateUserUseCase createUserUseCase
        )
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(
            [FromBody] CreateUserRequest request
        )
        {
            var traceId = Guid.NewGuid();
            var user = await _createUserUseCase.CreateUser(
                request.ToCommand(
                    traceId
                )
            );

            return Ok(user.FromUserResult());
        }
    }
}