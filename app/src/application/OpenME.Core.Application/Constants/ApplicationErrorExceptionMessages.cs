namespace OpenME.Core.Application.Constants
{
    public static class ApplicationErrorExceptionMessages
    {
        public static string UserCreateError(Guid traceId) => 
            $"Error occurred on user creation. Trace Id: {traceId}";
    }
}