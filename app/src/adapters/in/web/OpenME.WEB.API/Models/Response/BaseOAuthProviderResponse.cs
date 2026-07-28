using System.ComponentModel.DataAnnotations;

namespace OpenME.WEB.API.Models.Response
{
    public class BaseOAuthProviderResponse
    {
        public BaseOAuthProviderResponse(
            Guid id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}