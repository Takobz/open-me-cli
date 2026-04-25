using OpenME.Core.Application.Models.UseCases;

namespace OpenME.Core.Application.Ports.In
{
    public interface ICreateUserUseCase
    {
        public Task<CreateUserResult> CreateUser(CreateUserCommand command);
    }
}