namespace OpenME.Core.Application.Observability
{
    /// <summary>
    /// Interface to get traceId across layers.
    /// Implemented in the outer most layer.
    /// </summary>
    public interface ITraceContext
    {
        public Guid TraceId { get; }
    }
}