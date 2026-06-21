using System.ComponentModel.DataAnnotations;
using OpenME.Core.Application.Models.UseCases;

namespace OpenME.WEB.API.Models.Response
{
    public class CreateUserResponse : BaseUserResponse
    {
        public CreateUserResponse(
            Guid userId,
            string displayName,
            string email
        ) : base(userId, displayName, email) {}
    }
}