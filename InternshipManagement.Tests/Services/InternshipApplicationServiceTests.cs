using Application.DTOs.Applications;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using InternshipManagement.Tests.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;



namespace InternshipManagement.Tests.Services
{
    public class InternshipApplicationServiceTests
    {
        [Fact]
        public async Task ApplyAsync_ShouldCallRepositoryAndSave()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var appRepoMock = new Mock<Domain.Interfaces.IInternshipApplicationRepository>();
            unitOfWorkMock.SetupGet(u => u.InternshipApplications).Returns(appRepoMock.Object);
            unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var service = new InternshipApplicationService(unitOfWorkMock.Object);

            var dto = new CreateInternshipApplicationDto
            {
                StudentId = Guid.NewGuid(),
                InternshipId = Guid.NewGuid()
            };

            // Act
            await service.ApplyAsync(dto);

            // Assert
            appRepoMock.Verify(r => r.AddAsync(It.Is<InternshipApplication>(a => a.StudentId == dto.StudentId && a.InternshipId == dto.InternshipId)), Times.Once);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}