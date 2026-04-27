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
        public async Task UserService_Should_Throw_When_UserPort_Fails_To_Create_User()
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                Guid.NewGuid()
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<CreateUserDataCommand>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync(new BaseDataResult<CreateUserDataResult>(
                Data: null,
                IsSuccess: false
            ));

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
        [InlineData(TestInputData.EmptyGuidString, TestInputData.TestEmailAddress, TestInputData.TestDisplayName)]
        [InlineData(TestInputData.JustAGuid, "", TestInputData.TestDisplayName)]
        [InlineData(TestInputData.JustAGuid, TestInputData.TestEmailAddress, "")]
        public async Task UserService_Should_Return_None_Success_Result_When_UserPort_Create_User_With_Empty_Fields(
            string dataResultId,
            string dataResultEmail,
            string dataResultDisplayName
        )
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                traceId
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<CreateUserDataCommand>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync(new BaseDataResult<CreateUserDataResult>(
                Data: new CreateUserDataResult(
                    Guid.Parse(dataResultId),
                    dataResultEmail,
                    dataResultDisplayName
                ),
                IsSuccess: true
            ));

            var result = await _sut.CreateUser(new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName,
                traceId
            ));

            Assert.False(result.IsSuccess);
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
                It.Is<CreateUserDataCommand>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync(new BaseDataResult<CreateUserDataResult>(
                Data: new CreateUserDataResult(
                    Guid.NewGuid(),
                    TestInputData.TestEmailAddress,
                    TestInputData.TestDisplayName
                ),
                IsSuccess: true
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