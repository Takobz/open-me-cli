using OpenME.Core.Application.Models.UseCases;

namespace OpenME.WEB.API.Models.Response
{
    public class GetUserOAuthProvidersResponse
    {
        public GetUserOAuthProvidersResponse(
            GetUserOAuthProvidersResult providersResult
        )
        {
            OAuthProviders = providersResult.OAuthProviders.Select(x =>
            {
                return new BaseOAuthProviderResponse(
                    x.Id,
                    x.Name
                );
            });
        }

        public IEnumerable<BaseOAuthProviderResponse> OAuthProviders { get; set; }
    }
}
