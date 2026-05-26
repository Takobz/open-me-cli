using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OpenME.Core.Application.Observability;
using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Models;

namespace OpenME.Data.DatabaseProvider
{
    public class InMemoryDatabaseProvider : IDatabaseProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ITraceContext _traceContext;
        private readonly ILogger<InMemoryDatabaseProvider> _logger;

        public InMemoryDatabaseProvider(
            IMemoryCache memoryCache,
            ITraceContext traceContext,
            ILogger<InMemoryDatabaseProvider> logger
        )
        {
            _memoryCache = memoryCache;
            _traceContext = traceContext;
            _logger = logger;
        }

        public async Task<Me> CreateUser(IMeState meState)
        {
            if(_memoryCache.TryGetValue(meState.Id, out _))
            {
                _logger.LogDebug(
                    "{DatabaseProvider} found collision for UserId {UserId} for TraceId {TraceId}",
                    nameof(InMemoryDatabaseProvider),
                    meState.Id,
                    _traceContext.TraceId
                );

                // Ideally deal with collision but for now I will throw domain val exception.
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateExists(
                        meState.Id
                    )
                );
            }

            _memoryCache.Set(meState.Id, meState);

            _logger.LogDebug(
                "{DatabaseProvider} added user with UserId {UserId} for TraceId {TraceId}",
                nameof(InMemoryDatabaseProvider),
                meState.Id,
                _traceContext.TraceId
            );

            return await Task.FromResult( 
                Me.FromState(meState)
            );
        }
    }
}