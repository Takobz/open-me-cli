using System.ComponentModel.DataAnnotations;

namespace OpenME.WEB.API.Models.Response
{
    public class APIErrorResponse
    {
        [Required]
        public int StatusCode { get; private set; } = 500;

        [Required]
        public string[] ErrorMessages { get; private set; }  

        public APIErrorResponse(
            int statusCode,
            string[] errorMessages
        )
        {
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }
    }
}