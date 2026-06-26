using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;

namespace OpenME.Core.Domain.Models
{
    public interface IOAuthProviderState
    {
        public Guid Id { get; }

        public string Name { get; }

        public IEnumerable<OAuthProviderClientApp> ClientApps { get; }
    }

    public class OAuthProvider : IOAuthProviderState
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public IEnumerable<OAuthProviderClientApp> ClientApps { get; private set; } = [];

        private OAuthProvider(
            Guid id,
            string name,
            IEnumerable<OAuthProviderClientApp> clientApps
        )
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainValidationException(
                    OAuthProviderDomainValidationMessages.OAuthProviderNameEmpty
                );
            }

            Id = id;
            Name = name;
            ClientApps = clientApps;
        }

        public static OAuthProvider CreateProvider(
            string oauthProviderName
        )
        {
            return new OAuthProvider(
                Guid.NewGuid(),
                oauthProviderName,
                []
            );
        }

        public static OAuthProvider FromState(IOAuthProviderState state)
        {
            return new OAuthProvider(
                state.Id,
                state.Name,
                state.ClientApps
            );
        }

        public IOAuthProviderState GetState => this;
    }
}