namespace OpenME.Core.Domain.Constants
{
    public static class DomainValidationMessages
    {
        public static string OnUserCreateInCorrectEmail(string email) => 
            $"Provided email: {email} is not in correct format";
    }
}