using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Ports.Out
{
    public interface IGetOAuthProviderPort
    {
        Task<OAuthProvider?> GetOAuthProvider(Guid userId, Guid id);

        Task<IEnumerable<OAuthProvider>> GetUserOAuthProviders(Guid userId);
    }
}