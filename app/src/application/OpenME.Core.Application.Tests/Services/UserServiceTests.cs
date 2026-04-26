using Moq;
using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
using OpenME.Core.Application.Models.Data;
using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Application.Services;
using OpenME.Core.Application.Tests.Constants;

namespace OpenME.Core.Application.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<ICreateUserPort> _mockCreateUserPort;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _mockCreateUserPort = new Mock<ICreateUserPort>();
            _sut = new UserService(
                _mockCreateUserPort.Object
            );
        }

        [Fact]
        public async Task UserService_Should_Have_Unsuccessful_Result_When_We_Fail_To_Create_User(
        )
        {
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<CreateUserDataCommand>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync(new CreateUserDataResult(
                Guid.Empty,
                string.Empty,
                string.Empty
            ));

            var result = await _sut.CreateUser(command);

            Assert.False(result.IsSuccess);
            Assert.Equal(result.Id, Guid.Empty);
            Assert.Equal(result.Email, string.Empty);
            Assert.Equal(result.DisplayName, string.Empty);
        }
    }
}