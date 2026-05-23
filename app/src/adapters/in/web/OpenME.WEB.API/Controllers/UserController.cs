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
        //TODO: add base controller that logs traceId...
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
            var traceId = Guid.NewGuid(); //TODO: Add func to try get from header.
            var user = await _createUserUseCase.CreateUser(
                request.ToCommand(
                    traceId
                )
            );

            return Ok(user.FromUserResult());
        }
    }
}