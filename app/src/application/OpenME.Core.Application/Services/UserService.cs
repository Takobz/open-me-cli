using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
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
            
            var createdUser = await _createUserPort.CreateUser(
                user.GetMeState
            );

            if (createdUser == null)
            {
                throw new ApplicationErrorException(
                    ApplicationErrorExceptionMessages.UserCreateError(
                        command.TraceId
                    )
                );
            }

            return await Task.FromResult(
                new CreateUserResult(
                    createdUser.Id,
                    createdUser.DisplayName,
                    createdUser.Email
                )
            );
        }
    }
}