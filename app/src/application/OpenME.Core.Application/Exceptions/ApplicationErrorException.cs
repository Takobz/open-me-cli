namespace OpenME.Core.Application.Exceptions
{
    public class ApplicationErrorException : Exception
    {
        public string ErrorMessage { get; private set; }

        public ApplicationErrorException(
            string errorMessage
        )
        {
            ErrorMessage = errorMessage;
        }
    }
}