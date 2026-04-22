namespace OpenME.Core.Domain.Models
{
    public class OAuthProviderToken
    {
        public string AccessToken { get; private set; } = string.Empty;

        public string? RefreshToken { get; private set; } = string.Empty;

        public DateTimeOffset ExpiresIn { get; private set; }
    }
}