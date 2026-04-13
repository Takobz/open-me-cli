using OpenME.Core.Application.Models.UseCases;

namespace OpenME.Core.Application.Ports.In
{
    public interface ICreateOAuthAuthenticationLinkUseCase
    {
        public CreateOAuthLinkResult CreateOAuthLink(CreateOAuthLinkCommand command);
    }
}