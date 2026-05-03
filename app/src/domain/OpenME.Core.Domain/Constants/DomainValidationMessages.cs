namespace OpenME.Core.Domain.Constants
{
    public static class DomainValidationMessages
    {
        public const string OnUserCreateNoName = "Display Name cannot be empty";

        public const string OnUserCreateInvalidUUID = "Provided UUID is invalid";

        public static string OnUserCreateInCorrectEmail(string email) => 
            $"Provided email: {email} is not in correct format";

        public static string OnUserCreateExists(Guid id) =>
            $"User with id: {id} already exists";
    }
}