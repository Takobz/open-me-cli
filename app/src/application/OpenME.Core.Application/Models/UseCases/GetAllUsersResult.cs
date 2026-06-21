namespace OpenME.Core.Application.Models.UseCases
{
    public class GetAllUsersResult
    {
        public GetAllUsersResult(
            IEnumerable<GetAllUserUserResult> users
        )
        {
            Users = users;
        }

        public IEnumerable<GetAllUserUserResult> Users { get; private set; } = [];
    }

    public class GetAllUserUserResult
    {
        public Guid Id { get; private set; }

        public string DisplayName { get; private set; }

        public string Email { get; private set; }

        public GetAllUserUserResult(
            Guid id,
            string displayName,
            string email
        )
        {
            Id = id;
            DisplayName = displayName;
            Email = email;
        }  
    }
}