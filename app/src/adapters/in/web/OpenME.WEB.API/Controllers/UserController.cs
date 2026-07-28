using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OpenME.Core.Application.Models.UseCases;
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
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly ICreateOAuthProviderUseCase _createOAuthProviderUseCase;
        private readonly IGetOAuthProviderUseCase _getOAuthProviderUseCase;
        private readonly ILogger<UserController> _logger;
        private readonly ITraceContext _traceContext;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            IGetUserUseCase getUserUseCase,
            ICreateOAuthProviderUseCase createOAuthProviderUseCase,
            IGetOAuthProviderUseCase getOAuthProviderUseCase,
            ITraceContext traceContext,
            ILogger<UserController> logger
        )
        {
            _createUserUseCase = createUserUseCase;
            _getUserUseCase = getUserUseCase;
            _createOAuthProviderUseCase = createOAuthProviderUseCase;
            _getOAuthProviderUseCase = getOAuthProviderUseCase;
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

        [HttpGet]
        public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers()
        {
            _logger.LogDebug(
                "Executing GetAllUsers request Path: {UrlPath}, TraceId {TraceId}",
                HttpContext.Request.Path,
                _traceContext.TraceId
            );

            var users = await _getUserUseCase.GetAllUsers();
            return Ok(new GetAllUsersResponse(users));
        }

        [HttpPost]
        [Route("{userId}/providers")]
        public async Task<ActionResult<BaseOAuthProviderResponse>> CreateOAuthProvider(
            Guid userId,
            [FromBody] CreateOAuthProviderRequest request
        )
        {
            var result = await _createOAuthProviderUseCase.CreateOAuthProvider(
                new CreateOAuthProviderCommand(
                    request.OAuthProviderName,
                    userId
                )
            );

            if (!result.IsSuccess)
            {
                return Ok(new BaseOAuthProviderResponse(
                    Guid.Empty,
                    string.Empty
                ));
            }

            return Created(
                $"{HttpContext.Request.GetEncodedUrl()}/{result.Id}",
                result.FromOAuthProviderResult()
            );
        }

        [HttpGet]
        [Route("{userId}/providers")]
        public async Task<ActionResult<GetUserOAuthProvidersResponse>> GetOAuthProvidersByUserId(
            Guid userId
        )
        {
            _logger.LogDebug(
                "Executing GetOAuthProvidersByUserId request Path: {UrlPath}, TraceId {TraceId}",
                HttpContext.Request.Path,
                _traceContext.TraceId
            );

            var result = await _getOAuthProviderUseCase.GetUserOAuthProviders(
                userId
            );

            return Ok(new GetUserOAuthProvidersResponse(result));
        }

        [HttpGet]
        [Route("{userId}/providers/{providerId}")]
        public async Task<ActionResult<BaseOAuthProviderResponse>> GetOAuthProviderById(
            Guid userId,
            Guid providerId
        )
        {
            _logger.LogDebug(
                "Executing GetOAuthProviderById request Path: {UrlPath}, TraceId {TraceId}",
                HttpContext.Request.Path,
                _traceContext.TraceId
            );

            var result = await _getOAuthProviderUseCase.GetOAuthProvider(
                userId,
                providerId
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.FromOAuthProviderResult());
        }
    }
}