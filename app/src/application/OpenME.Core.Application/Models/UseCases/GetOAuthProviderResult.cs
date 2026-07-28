namespace OpenME.Core.Application.Models.UseCases
{
    public class GetOAuthProviderResult
    {
        public GetOAuthProviderResult(
            Guid id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}