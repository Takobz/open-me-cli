using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Extensions;

namespace OpenME.Core.Domain.Models
{
    public class Me
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; } = string.Empty;

        public IEnumerable<OAuthProvider> OAuthProviders { get; private set; } = [];

        private Me(
            Guid id,
            string email,
            IEnumerable<OAuthProvider> oAuthProviders
        )
        {
            Id = id;
            Email = email;
            OAuthProviders = oAuthProviders;
        }

        public static Me CreateMe(
            string email
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
                email,
                []
            );
        }
    } 
}
