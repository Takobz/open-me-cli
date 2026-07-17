using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Services
{
    public class OAuthProviderService : ICreateOAuthProviderUseCase
    {
        private readonly ICreateOAuthProviderPort _createOAuthProviderPort;
        private readonly IGetUserPort _getUserPort;

        public OAuthProviderService(
            ICreateOAuthProviderPort createOAuthProviderPort,
            IGetUserPort getUserPort
        )
        {
            _createOAuthProviderPort = createOAuthProviderPort;
            _getUserPort = getUserPort;
        }

        public async Task<CreateOAuthProviderResult> CreateOAuthProvider(CreateOAuthProviderCommand command)
        {
            var user = await _getUserPort.GetMeById(command.UserId);
            if (user == null)
            {
                return new CreateOAuthProviderResult(
                    Guid.Empty,
                    string.Empty
                );
            }

            var oauthProvider = OAuthProvider.CreateProvider(
                command.OAuthProviderName,
                command.UserId
            );

            var createdProvider = await _createOAuthProviderPort.CreateOAuthProvider(
                oauthProvider.GetState
            );

            if (createdProvider == null)
            {
                return new CreateOAuthProviderResult(
                    Guid.Empty,
                    string.Empty
                );
            }

            return new CreateOAuthProviderResult(
                createdProvider.Id,
                createdProvider.Name
            );
        }
    }
}