using OpenME.Core.Domain.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Domain.Tests.Models
{
    public class MeTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("test@email")]
        [InlineData("test.com")]
        [InlineData("string")]
        public void CreateMe_With_Invalid_Email_Should_Fail(
            string inputEmail
        )
        {
            var exception = Assert.Throws<DomainValidationException>(
                () => Me.CreateMe(
                    inputEmail
                )
            );

            Assert.Equal(
                DomainValidationMessages.OnUserCreateInCorrectEmail(
                    inputEmail
                ),
                exception.ValidationMessage
            );
        }

        [Theory]
        [InlineData("test.tester@emal.com")]
        [InlineData("test@emal.com")]
        [InlineData("test.tester@emal.co.za")]
        public void CreateMe_With_Valid_Email_Should_Create_User(
            string inputEmail
        )
        {
            var result = Me.CreateMe(inputEmail);
            Assert.Equal(inputEmail, result.Email);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Empty(result.OAuthProviders);
        } 
    }
}