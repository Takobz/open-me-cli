namespace OpenME.Core.Application.Models.UseCases
{
    public class GetUserOAuthProvidersResult
    {
        public GetUserOAuthProvidersResult(
            IEnumerable<GetOAuthProviderResult> results
        )
        {
            OAuthProviders = results;
        }

        public IEnumerable<GetOAuthProviderResult> OAuthProviders { get; set; } = [];
    }
}