namespace OpenME.Core.Domain.Models
{
    public class OAuthProvider
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public IEnumerable<OAuthProviderClientApp> ClientApps { get; private set; } = [];

        public IEnumerable<OAuthProviderToken> OAuthTokens { get; private set; } = [];
    }
}