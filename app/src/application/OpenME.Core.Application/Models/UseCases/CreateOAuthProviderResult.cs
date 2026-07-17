namespace OpenME.Core.Application.Models.UseCases
{
    public class CreateOAuthProviderResult
    {
        public CreateOAuthProviderResult(
            Guid id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool IsSuccess { get 
            { return (
                Id != Guid.Empty ||
                !string.IsNullOrEmpty(Name));
            } 
        }
    }
}