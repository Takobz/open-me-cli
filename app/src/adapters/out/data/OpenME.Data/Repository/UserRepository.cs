using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;
using OpenME.Data.DatabaseProvider;

namespace OpenME.Data.Repository
{
    public class UserRepository : ICreateUserPort
    {
        private readonly IDatabaseProvider _databaseProvider;

        public UserRepository(
            IDatabaseProvider databaseProvider
        )
        {
            _databaseProvider = databaseProvider;
        }

        public async Task<Me?> CreateUser(IMeState command)
        {
            return await _databaseProvider.CreateUser(
                command
            );
        }
    }
}