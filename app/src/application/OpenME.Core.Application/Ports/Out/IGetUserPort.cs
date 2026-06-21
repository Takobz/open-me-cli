using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Ports.Out
{
    public interface IGetUserPort
    {
        Task<IEnumerable<Me>> GetAllMes();

        Task<Me?> GetMeById(Guid Id);
    }
}