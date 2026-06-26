using OpenME.Core.Domain.Constants;

namespace OpenME.Core.Domain.Models
{
    public class OAuthProviderClientApp
    {
        public Guid Id { get; private set; }

        public string OAuthAppName { get; private set; } = string.Empty;

        public string OAuthClientId { get; private set; } = string.Empty;

        public string OAuthClientSecret { get; private set; } = string.Empty;

        public IEnumerable<OAuthProviderToken> OAuthTokens { get; private set; } = [];

        public string OAuthVersion { get; private set; } = StringConstants.OAUTH_VERSION_2;
    }
}