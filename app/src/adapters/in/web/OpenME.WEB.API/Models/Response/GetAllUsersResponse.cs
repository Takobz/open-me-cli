using OpenME.Core.Application.Models.UseCases;
using OpenME.Data.Models;

namespace OpenME.WEB.API.Models.Response
{
    public class GetAllUsersResponse
    {
        public GetAllUsersResponse(
            GetAllUsersResult usersResult
        )
        {
            Users = usersResult.Users.Select(x =>
            {
               return new BaseUserResponse(
                x.Id,
                x.DisplayName,
                x.Email
               ); 
            });
        }

        public IEnumerable<BaseUserResponse> Users { get; set; }
    }
}