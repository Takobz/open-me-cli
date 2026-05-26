using OpenME.Core.Application.Observability;
using OpenME.WEB.API.Constants;

namespace OpenME.WEB.API.Contexts
{
    /// <summary>
    /// Register as Scoped in DI to have one traceId per request.
    /// </summary>
    public class TraceContext : ITraceContext
    {
        private readonly Guid _traceId;

        public TraceContext(
            IHttpContextAccessor httpContextAccessor
        )
        {
            if (
                httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(
                    HeaderConstants.TraceIdHeaderKey, 
                    out var traceId) &&
                Guid.TryParse(traceId, out var result)
            )
            {
                _traceId = result;
            }
            else
            {
                _traceId = Guid.NewGuid();
            } 
        }

        public Guid TraceId => _traceId;
    }
}