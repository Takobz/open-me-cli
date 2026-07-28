namespace OpenME.Core.Application.Models.UseCases
{
    public class CreateOAuthProviderCommand
    {
        public CreateOAuthProviderCommand(
            string providerName,
            Guid userId
        )
        {
            OAuthProviderName = providerName;
            UserId = userId;
        }

        public string OAuthProviderName { get; private set; }
        public Guid UserId { get; private set; }
    }
}