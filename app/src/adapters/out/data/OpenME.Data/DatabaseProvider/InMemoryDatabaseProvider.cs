using Microsoft.Extensions.Caching.Memory;
using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Models;

namespace OpenME.Data.DatabaseProvider
{
    public class InMemoryDatabaseProvider : IDatabaseProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryDatabaseProvider(
            IMemoryCache memoryCache
        )
        {
            _memoryCache = memoryCache;
        }

        public async Task<Me> CreateUser(IMeState meState)
        {
            if(_memoryCache.TryGetValue(meState.Id, out _))
            {
                // Ideally deal with collision but for now I will throw domain val exception.
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateExists(
                        meState.Id
                    )
                );
            }

            _memoryCache.Set(meState.Id, meState);
            return await Task.FromResult( 
                Me.FromState(meState)
            );
        }
    }
}