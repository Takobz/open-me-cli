using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Ports.Out
{
    /// <summary>
    /// Out port will be implemented outside the core
    /// Some database implementation will happen.
    /// </summary>
    public interface ICreateUserPort
    {
        Task<Me?> CreateUser(IMeState command);
    }
}