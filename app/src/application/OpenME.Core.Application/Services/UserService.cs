using Microsoft.Extensions.Logging;
using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Observability;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Services
{
    public class UserService : ICreateUserUseCase, IGetUserUseCase
    {
        private readonly ICreateUserPort _createUserPort;
        private readonly IGetUserPort _getUserPort;
        private readonly ITraceContext _traceContext;
        private readonly ILogger<UserService> _logger;

        public UserService(
            ICreateUserPort createUserPort,
            IGetUserPort getUserPort,
            ITraceContext traceContext,
            ILogger<UserService> logger
        )
        {
            _createUserPort = createUserPort;
            _getUserPort = getUserPort;
            _traceContext = traceContext;
            _logger = logger;
        }

        public async Task<CreateUserResult> CreateUser(CreateUserCommand command)
        {
            _logger.LogDebug(
                "{UseCase} Creating user with Email: {UserEmail} and DisplayName: {UserDisplayName} for TraceId {TraceId}",
                nameof(ICreateUserUseCase),
                command.Email,
                command.DisplayName,
                _traceContext.TraceId
            );

            var user = Me.CreateMe(
                command.Email,
                command.DisplayName
            );
            
            var createdUser = await _createUserPort.CreateUser(
                user.GetMeState
            );

            if (createdUser == null)
            {
                throw new ApplicationErrorException(
                    ApplicationErrorExceptionMessages.UserCreateError(
                        _traceContext.TraceId
                    )
                );
            }

            _logger.LogDebug(
                "{UseCase} created user with UserId {UserId} for TraceId {TraceId}",
                nameof(ICreateUserUseCase),
                createdUser.Id,
                _traceContext.TraceId
            );

            return await Task.FromResult(
                new CreateUserResult(
                    createdUser.Id,
                    createdUser.DisplayName,
                    createdUser.Email
                )
            );
        }

        public async Task<GetAllUsersResult> GetAllUsers()
        {
            _logger.LogDebug(
                "{UseCase} Getting all users for or TraceId {TraceId}",
                nameof(ICreateUserUseCase),
                _traceContext.TraceId
            );

            var mes = await _getUserPort.GetAllMes();
            var users = mes.Select(x => new GetAllUserUserResult(
                x.Id,
                x.DisplayName,
                x.Email
            ));

            _logger.LogDebug(
                "{UseCase} Got {GetAllUsersCount} users for or TraceId {TraceId}",
                nameof(ICreateUserUseCase),
                users.Count(),
                _traceContext.TraceId
            );

            return await Task.FromResult(
                new GetAllUsersResult(users)
            );
        }
    }
}