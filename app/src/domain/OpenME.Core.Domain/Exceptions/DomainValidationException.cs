namespace OpenME.Core.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public string ValidationMessage { get; private set; }

        public DomainValidationException(
            string validationMessage
        ) : base(validationMessage)
        {
            ValidationMessage = validationMessage;
        }
    }
}