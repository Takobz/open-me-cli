using Microsoft.Extensions.Logging;
using Moq;
using OpenME.Core.Application.Constants;
using OpenME.Core.Application.Exceptions;
using OpenME.Core.Application.Models.UseCases;
using OpenME.Core.Application.Observability;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Application.Services;
using OpenME.Core.Application.Tests.Constants;
using OpenME.Core.Domain.Models;

namespace OpenME.Core.Application.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<ICreateUserPort> _mockCreateUserPort;
        private readonly Mock<IGetUserPort> _mockGetUserPort;
        private readonly Mock<ITraceContext> _mockTraceContext;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _mockCreateUserPort = new Mock<ICreateUserPort>();
            _mockGetUserPort = new Mock<IGetUserPort>();
            _mockTraceContext = new Mock<ITraceContext>();
            _mockLogger = new Mock<ILogger<UserService>>();

            _sut = new UserService(
                _mockCreateUserPort.Object,
                _mockGetUserPort.Object,
                _mockTraceContext.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task UserService_Should_Throw_When_UserPort_Fails_To_Create_User()
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName
            );

            _mockCreateUserPort.Setup(_ => _.CreateUser(
                It.Is<IMeState>(cmd =>
                    cmd.DisplayName == TestInputData.TestDisplayName &&
                    cmd.Email == TestInputData.TestEmailAddress
                )
            )).ReturnsAsync((Me)null!);

            _mockTraceContext.Setup(_ => _.TraceId)
                .Returns(traceId);

            var exception = await Assert.ThrowsAsync<ApplicationErrorException>(
                () => _sut.CreateUser(new CreateUserCommand(
                    TestInputData.TestEmailAddress,
                    TestInputData.TestDisplayName
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
                inputDisplayName
            )));
        }

        [Fact]
        public async Task UserService_Should_Return_Success_Result_When_UserPort_Creates_User()
        {
            var traceId = Guid.NewGuid();
            var command = new CreateUserCommand(
                TestInputData.TestEmailAddress,
                TestInputData.TestDisplayName
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
                TestInputData.TestDisplayName
            ));

            Assert.True(result.IsSuccess);
        }
    }
}