namespace OpenME.Core.Application.Models.Data
{
    public record CreateUserDataCommand(
        Guid Id,
        string DisplayName,
        string Email
    );
}