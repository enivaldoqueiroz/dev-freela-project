using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Moq;

namespace DevFreela.UnitTests.Application.Commonds
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Uma descrição aqui",
                TotalCost = 50000,
                IdClient = 1,
                IdFreela = 2
            };

            var createProjectCommadHandler = new CreateProjectCommandHandler(unitOfWorkMock.Object);

            // Act
            var id = await createProjectCommadHandler.Handle(createProjectCommand, new CancellationToken());

            // Assert
            Assert.True(id >= 0);

            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
