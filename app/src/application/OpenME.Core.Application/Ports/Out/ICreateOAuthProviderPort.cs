using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Ports.Out
{
    public interface ICreateOAuthProviderPort
    {
        public Task<OAuthProvider?> CreateOAuthProvider(IOAuthProviderState state);
    }
}