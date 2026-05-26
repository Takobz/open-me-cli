using Microsoft.AspNetCore.Mvc;
using OpenME.Core.Application.Observability;
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
        private readonly ILogger<UserController> _logger;
        private readonly ITraceContext _traceContext;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            ITraceContext traceContext,
            ILogger<UserController> logger
        )
        {
            _createUserUseCase = createUserUseCase;
            _traceContext = traceContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(
            [FromBody] CreateUserRequest request
        )
        {
            _logger.LogDebug(
                "Executing CreateUser request Path: {UrlPath}, TraceId {TraceId}",
                HttpContext.Request.Path,
                _traceContext.TraceId
            );

            var user = await _createUserUseCase.CreateUser(
                request.ToCommand()
            );

            return Ok(user.FromUserResult());
        }
    }
}