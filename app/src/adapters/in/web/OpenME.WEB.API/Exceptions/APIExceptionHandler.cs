using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using OpenME.Core.Domain.Exceptions;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Exceptions
{
    public class APIExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken
        )
        {
            if (exception is DomainValidationException)
            {
                var domainException = exception as DomainValidationException;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(
                    new APIErrorResponse(
                        (int)HttpStatusCode.BadRequest,
                        [domainException!.ValidationMessage]
                    ),
                    cancellationToken
                );
            }

            return true;
        }
    }
}