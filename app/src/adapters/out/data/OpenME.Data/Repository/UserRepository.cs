using OpenME.Core.Application.Models.Data;
using OpenME.Core.Application.Ports.Out;
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

        public Task<BaseDataResult<CreateUserDataResult>> CreateUser(CreateUserDataCommand command)
        {
            throw new NotImplementedException();
        }
    }
}