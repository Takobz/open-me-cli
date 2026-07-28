using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;
using OpenME.Data.DatabaseProvider;

namespace OpenME.Data.Repository
{
    public class UserRepository :
        ICreateUserPort,
        IGetUserPort,
        ICreateOAuthProviderPort,
        IGetOAuthProviderPort
    {
        private readonly IDatabaseProvider _databaseProvider;

        public UserRepository(
            IDatabaseProvider databaseProvider
        )
        {
            _databaseProvider = databaseProvider;
        }

        public async Task<OAuthProvider?> CreateOAuthProvider(IOAuthProviderState state)
        {
            return await _databaseProvider.CreateOAuthProvider(
                state
            );
        }

        public async Task<Me?> CreateUser(IMeState command)
        {
            return await _databaseProvider.CreateUser(
                command
            );
        }

        public async Task<IEnumerable<Me>> GetAllMes()
        {
            return await _databaseProvider.GetAllMes();
        }

        public async Task<Me?> GetMeById(Guid Id)
        {
            return await _databaseProvider.GetMeById(Id);
        }

        public async Task<OAuthProvider?> GetOAuthProvider(Guid userId, Guid id)
        {
            return await _databaseProvider.GetOAuthProvider(
                userId,
                id
            );
        }

        public async Task<IEnumerable<OAuthProvider>> GetUserOAuthProviders(Guid userId)
        {
            return await _databaseProvider.GetUserOAuthProviders(
                userId
            );
        }
    }
}