using System.ComponentModel.DataAnnotations;

namespace OpenME.WEB.API.Models.Request
{
    public class CreateOAuthProviderRequest
    {
        [Required]
        [MinLength(3)]
        public string OAuthProviderName { get; set; } = string.Empty;
    }
}