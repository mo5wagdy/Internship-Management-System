using System;
using System.Threading.Tasks;
using Application.DTOs.Users;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Moq;
using Xunit;

namespace InternshipManagement.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterAsync_Should_SaveUser_AndReturnAuth()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<Domain.Interfaces.IUserRepository>();
            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepoMock.Object);
            unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var jwtMock = new Mock<IJwtTokenGenerator>();
            jwtMock.Setup(j => j.GenerateToken(It.IsAny<User>()))
                   .Returns(new TokenResult { Token = "token123", Expiration = DateTime.UtcNow.AddHours(1) });

            var service = new UserService(unitOfWorkMock.Object, jwtMock.Object);

            var dto = new UserRegisterDto
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "password123",
                Role = "Student"
            };

            // Act
            var result = await service.RegisterAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal("Student", result.Role);
            Assert.Equal("token123", result.Token);

            userRepoMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == dto.Email)), Times.Once);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_InvalidCredentials_Throws()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<Domain.Interfaces.IUserRepository>();
            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepoMock.Object);

            userRepoMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var jwtMock = new Mock<IJwtTokenGenerator>();
            var service = new UserService(unitOfWorkMock.Object, jwtMock.Object);

            var dto = new UserLoginDto { Email = "noone@example.com", Password = "pass" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.LoginAsync(dto));
        }
    }
}