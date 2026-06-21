using OpenME.Core.Domain.Models;

namespace OpenME.Data.DatabaseProvider
{
    /// <summary>
    /// Operates on Database models can be impl'd by diff databases (InMemory, Postgres, etc)
    /// </summary>
    public interface IDatabaseProvider
    {
        public Task<Me> CreateUser(IMeState meState);

        public Task<IEnumerable<Me>> GetAllMes();

        public Task<Me?> GetMeById(Guid Id);
    }
}