namespace OpenME.Core.Application.Models.UseCases
{
    public class CreateOAuthProviderCommand
    {
        public CreateOAuthProviderCommand(
            string providerName
        )
        {
            OAuthProviderName = providerName;
        }

        public string OAuthProviderName { get; private set; }
    }
}