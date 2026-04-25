namespace OpenME.Core.Application.Models.Data
{
    /// <summary>
    /// Expected data from data adaptor.
    /// Used by out ports
    /// </summary>
    public record CreateUserDataResult(
        Guid Id,
        string DisplayName,
        string Email
    );
}