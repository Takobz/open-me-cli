using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.In;

namespace OpenME.Core.Application.Services
{
    /// <summary>
    /// Application services implement in-ports.
    /// </summary>
    public class OAuthConfiguration : ICreateOAuthAuthenticationLinkUseCase
    {
        public CreateOAuthLinkResult CreateOAuthLink(CreateOAuthLinkCommand command)
        {
            throw new NotImplementedException();
        }
    }
}