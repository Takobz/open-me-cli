using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Services
{
    public class OAuthProviderService : ICreateOAuthProviderUseCase
    {
        private readonly ICreateOAuthProviderPort _createOAuthProviderPort;

        public OAuthProviderService(
            ICreateOAuthProviderPort createOAuthProviderPort
        )
        {
            _createOAuthProviderPort = createOAuthProviderPort;
        }

        public async Task<OAuthProvider?> CreateOAuthProvider(CreateOAuthProviderCommand command)
        {
            var oauthProvider = OAuthProvider.CreateProvider(
                command.OAuthProviderName
            );

            return await _createOAuthProviderPort.CreateOAuthProvider(
                oauthProvider.GetState
            );
        }
    }
}