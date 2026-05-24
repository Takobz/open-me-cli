using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Errors
{
    /// <summary>
    /// The Implementation called when a user sends a bad request.
    /// </summary>
    public class APIClientErrorFactory : IClientErrorFactory
    {
        public IActionResult? GetClientError(
            ActionContext actionContext, 
            IClientErrorActionResult clientError
        )
        {
            if (clientError.StatusCode != (int)HttpStatusCode.BadRequest)
            {
                return new ObjectResult(new Simple4xxResponse())
                {
                    StatusCode = clientError.StatusCode
                };
            }

            var modelStateErrorValues = actionContext.ModelState.Values;
            IEnumerable<string> errors = [];
            foreach(var value in modelStateErrorValues)
            {
                var errorMessages = value.Errors.Select(e => e.ErrorMessage);
                errors = errors.Concat(errorMessages);
            }

            return new BadRequestObjectResult(
                new BadRequestResponse(
                    [.. errors]
                )
            );
        }
    }
}