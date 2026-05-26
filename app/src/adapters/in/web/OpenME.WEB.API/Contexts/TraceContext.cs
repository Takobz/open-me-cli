using OpenME.Core.Application.Observability;

namespace OpenME.WEB.API.Contexts
{
    /// <summary>
    /// Register as Scoped in DI to have one traceId per request.
    /// </summary>
    public class TraceContext : ITraceContext
    {
        private readonly Guid _traceId;

        public TraceContext()
        {
           //TODO: Add httpContextAccessor to try read header. 
           _traceId = Guid.NewGuid();
        }

        public Guid TraceId => _traceId;
    }
}