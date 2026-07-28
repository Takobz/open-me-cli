using OpenME.Core.Application.Models.UseCases;

namespace OpenME.Core.Application.Ports.In
{
    public interface IGetOAuthProviderUseCase
    {
        public Task<GetOAuthProviderResult?> GetOAuthProvider(Guid userId, Guid id);

        public Task<GetUserOAuthProvidersResult> GetUserOAuthProviders(Guid userId);
    }
}