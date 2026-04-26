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

            //TODO: update the port interface to use the result pattern so we don't accidentally null refs
            var createdUser = await _createUserPort.CreateUser(
                new CreateUserDataCommand(
                    user.Id,
                    user.DisplayName,
                    command.Email
                )
            );

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