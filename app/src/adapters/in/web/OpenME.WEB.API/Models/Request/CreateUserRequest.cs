using System.ComponentModel.DataAnnotations;
using OpenME.Core.Application.Models.UseCases;

namespace OpenME.WEB.API.Models.Request
{
    public class CreateUserRequest
    {
        [Required]
        [Length(5, 100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public CreateUserCommand ToCommand(
            Guid traceId
        )
        {
            return new CreateUserCommand(
                Email,
                DisplayName,
                traceId
            );
        }
    }
}