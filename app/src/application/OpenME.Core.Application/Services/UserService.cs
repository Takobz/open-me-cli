using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
using OpenME.Core.Application.Models.Data;
using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Services
{
    public class UserService : ICreateUserUseCase
    {
        private readonly ICreateUserPort _createUserPort;

        public UserService(
            ICreateUserPort createUserPort
        )
        {
            _createUserPort = createUserPort;
        }

        public async Task<CreateUserResult> CreateUser(CreateUserCommand command)
        {
            var user = Me.CreateMe(
                command.Email,
                command.DisplayName
            );
            
            var createdUserResult = await _createUserPort.CreateUser(
                new CreateUserDataCommand(
                    user.Id,
                    user.DisplayName,
                    command.Email
                )
            );

            if (!createdUserResult.IsSuccess || createdUserResult.Data == null)
            {
                throw new ApplicationErrorException(
                    ApplicationErrorExceptionMessages.UserCreateError(
                        command.TraceId
                    )
                );
            }

            return await Task.FromResult(
                new CreateUserResult(
                    createdUserResult.Data.Id,
                    createdUserResult.Data.DisplayName,
                    createdUserResult.Data.Email
                )
            );
        }
    }
}