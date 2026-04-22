using System.Text.RegularExpressions;
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
                //TODO throw domain validation exception
            }

            return new Me(
                Guid.NewGuid(),
                email,
                []
            );
        }        
    } 
}