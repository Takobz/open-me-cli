using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;
using OpenME.Data.DatabaseProvider;

namespace OpenME.Data.Repository
{
    public interface IUserRepository : ICreateUserPort
    {
        
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseProvider _databaseProvider;

        public UserRepository(
            IDatabaseProvider databaseProvider
        )
        {
            _databaseProvider = databaseProvider;
        }

        public Task<Me?> CreateUser(IMeState command)
        {
            throw new NotImplementedException();
        }
    }
}