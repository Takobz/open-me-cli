using OpenME.Core.Application.Models.Data;

namespace OpenME.Core.Application.Ports.Out
{
    /// <summary>
    /// Out port will be implemented outside the core
    /// Some database implementation will happen.
    /// </summary>
    public interface ICreateUserPort
    {
        Task<CreateUserDataResult> CreateUser(CreateUserDataCommand command);
    }
}