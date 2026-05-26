using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;

namespace OpenME.Core.Application.Models.UseCases
{
    public class CreateUserCommand
    {
        public string Email { get; private set; }
        public string DisplayName { get; private set; }

        public CreateUserCommand(
            string email,
            string displayName
        )
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationInputValidationException(
                    InputValidationExceptionMessages.EmailEmpty
                );
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new ApplicationInputValidationException(
                    InputValidationExceptionMessages.DisplayNameEmpty
                );
            }

            Email = email;
            DisplayName = displayName;
        }
    }
}