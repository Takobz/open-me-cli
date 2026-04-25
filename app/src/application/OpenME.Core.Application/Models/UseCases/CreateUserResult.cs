namespace OpenME.Core.Application.Models.UseCases
{
    public class CreateUserResult
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return (
                    Id != Guid.Empty &&
                    !string.IsNullOrEmpty(DisplayName) &&
                    !string.IsNullOrEmpty(Email)
                );
            }
        }

        public CreateUserResult(
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