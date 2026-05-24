using System.ComponentModel.DataAnnotations;

namespace OpenME.WEB.API.Models.Response
{
    public class BadRequestResponse
    {
        [Required]
        public string[] ErrorMessages { get; private set; }  

        public BadRequestResponse(
            string[] errorMessages
        )
        {
            ErrorMessages = errorMessages;
        }
    }

    public class Simple4xxResponse
    {
        
    }
}