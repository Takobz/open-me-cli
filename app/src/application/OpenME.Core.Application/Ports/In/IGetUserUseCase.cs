using OpenME.Core.Application.Models.UseCases;

namespace OpenME.Core.Application.Ports.In
{
    public interface IGetUserUseCase
    {
        public Task<GetAllUsersResult> GetAllUsers();
    }
} 