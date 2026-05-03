using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Extensions;

namespace OpenME.Core.Domain.Models
{
    public interface IMeState
    {
        public Guid Id { get; }

        public string DisplayName { get; }

        public string Email { get; }
    }

    public class Me : IMeState
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
            return FromState(
                new Me(
                    Guid.NewGuid(),
                    displayName,
                    email,
                    []
                ).GetMeState
            );
        }

        public static Me FromState(IMeState state)
        {
            var email = state.Email;
            if (email == string.Empty || !email.IsEmailCorrectFormat())
            {
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateInCorrectEmail(
                        email
                    )
                );
            }
            
            var displayName = state.DisplayName;
            if (string.IsNullOrEmpty(displayName))
            {
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateNoName
                );
            }

            var userId = state.Id;
            if (userId == Guid.Empty)
            {
                throw new DomainValidationException(
                    DomainValidationMessages.OnUserCreateInvalidUUID
                );
            }

            return new Me(
                state.Id,
                state.DisplayName,
                email,
                [] //TODO: remove from user creation useless
            );
        }

        public IMeState GetMeState => this;
    } 
}
