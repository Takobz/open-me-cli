using Moq;
using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Application.Services;
using OpenME.Core.Application.Tests.Constants;
using OpenME.Core.Domain.Exceptions;
using OpenME.Core.Domain.Models;

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
        public async Task UserService_Should_Throw_When_UserPort_Fails_To_Create_User()
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                Guid.NewGuid()
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<IMeState>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync((Me)null!);

            var exception = await Assert.ThrowsAsync<ApplicationErrorException>(
                () => _sut.CreateUser(new CreateUserCommand(
                    TestInputData.TestEmailAddress,
                    TestInputData.TestDisplayName,
                    traceId
                ))
            );

            Assert.Equal(
                ApplicationErrorExceptionMessages.UserCreateError(
                    traceId
                ),
                exception.ErrorMessage
            );
        }

        [Theory]
        [InlineData("", TestInputData.TestDisplayName)]
        [InlineData(TestInputData.TestEmailAddress, "")]
        public async Task UserService_Should_Throw_Input_Validation_When_Command_Has_Empty_Fields(
            string inputEmail,
            string inputDisplayName
        )
        {
            await Assert.ThrowsAsync<ApplicationInputValidationException>(() => _sut.CreateUser(
                new CreateUserCommand(
                inputEmail,
                inputDisplayName,
                Guid.NewGuid()
            )));
        }

        [Fact]
        public async Task UserService_Should_Return_Success_Result_When_UserPort_Creates_User()
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                traceId
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<IMeState>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync(Me.CreateMe(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName
            ));

            var result = await _sut.CreateUser(new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                traceId
            ));

            Assert.True(result.IsSuccess);
        }
    }
}