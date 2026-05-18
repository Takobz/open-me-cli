using System.ComponentModel.DataAnnotations;

namespace OpenME.WEB.API.Models.Response
{
    public class CreateUserResponse
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Length(0, 100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}