using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Services
{
    public class OAuthProviderService : ICreateOAuthProviderUseCase, IGetOAuthProviderUseCase
    {
        private readonly ICreateOAuthProviderPort _createOAuthProviderPort;
        private readonly IGetOAuthProviderPort _getOAuthProviderPort;
        private readonly IGetUserPort _getUserPort;

        public OAuthProviderService(
            ICreateOAuthProviderPort createOAuthProviderPort,
            IGetOAuthProviderPort getOAuthProviderPort,
            IGetUserPort getUserPort
        )
        {
            _createOAuthProviderPort = createOAuthProviderPort;
            _getOAuthProviderPort = getOAuthProviderPort;
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

        public async Task<GetOAuthProviderResult?> GetOAuthProvider(Guid userId, Guid id)
        {
            var provider = await _getOAuthProviderPort.GetOAuthProvider(
                userId,
                id
            );

            if (provider == null)
            {
                return null;
            }

            return new GetOAuthProviderResult(
                provider.Id,
                provider.Name
            );
        }

        public async Task<GetUserOAuthProvidersResult> GetUserOAuthProviders(Guid userId)
        {
            var providers = await _getOAuthProviderPort.GetUserOAuthProviders(
                userId
            );

            var results = providers.Select(x => new GetOAuthProviderResult(
                x.Id,
                x.Name
            ));

            return new GetUserOAuthProvidersResult(
                results
            );
        }
    }
}