namespace OpenME.Core.Application.Exceptions
{
    public class ApplicationInputValidationException : Exception
    {
        public string InputValidationExceptionMessage { get; private set; }

        public ApplicationInputValidationException(
            string inputValidationExceptionMessage
        ) : base()
        {
            InputValidationExceptionMessage = inputValidationExceptionMessage;
        }
    }
}