using Microsoft.AspNetCore.Mvc;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomClientErrorOptions(
            this IMvcBuilder builder
        )
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var modelStateErrorValues = actionContext.ModelState.Values;
                    IEnumerable<string> errors = [];
                    foreach (var value in modelStateErrorValues)
                    {
                        var errorMessages = value.Errors.Select(e => e.ErrorMessage);
                        errors = errors.Concat(errorMessages);
                    }

                    return new BadRequestObjectResult(
                        new BadRequestResponse(
                            [.. errors]
                        )
                    );
                };
            });

            return builder;
        }
    }
}