using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Extensions;

namespace OpenME.Core.Domain.Models
{
    public class Me
    {
        public Guid Id { get; private set; }

        public string DisplayName { get; private set; }

        public string Email { get; private set; }

        public IEnumerable<OAuthProvider> OAuthProviders { get; private set; } = [];

        private Me(
            Guid id,
            string displayName,
            string email,
            IEnumerable<OAuthProvider> oAuthProviders
        )
        {
            Id = id;
            DisplayName = displayName;
            Email = email;
            OAuthProviders = oAuthProviders;
        }

        public static Me CreateMe(
            string email,
            string displayName
        )
        {
            if (email == string.Empty || !email.IsEmailCorrectFormat())
            {
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateInCorrectEmail(
                        email
                    )
                );
            }

            return new Me(
                Guid.NewGuid(),
                displayName,
                email,
                [] //TODO: remove from user creation useless
            );
        }
    } 
}
