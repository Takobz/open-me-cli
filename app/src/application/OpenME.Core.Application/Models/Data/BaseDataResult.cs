namespace OpenME.Core.Application.Models.Data
{
    /// <summary>
    ///  Base record to be used by data layer adapter's implementation.
    /// </summary>
    /// <typeparam name="TData">A type representing expect data from the data layer</typeparam>
    /// <param name="Data">Optional data return by a query or command from the data layer</param>
    /// <param name="IsSuccess">Flag indicating if the data layer successfully did an operation</param>
    public record BaseDataResult<TData>(
        TData? Data,
        bool IsSuccess
    );
}