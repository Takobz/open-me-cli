using OpenME.Core.Application.Models.UseCases;
using OpenME.WEB.API.Models.Response;

namespace OpenME.WEB.API.Models
{
    public static class Mapper
    {
        public static CreateUserResponse FromUserResult(
            this CreateUserResult result
        )
        {
            //TODO: use to result.IsSuccess....
            return new CreateUserResponse(
                result.Id,
                result.DisplayName,
                result.Email
            );
        }

        public static CreateOAuthProviderResponse FromOAuthProviderResult(
            this CreateOAuthProviderResult result
        )
        {
            return new CreateOAuthProviderResponse(
                result.Id,
                result.Name
            );
        }
    }
}