using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = new Project(request.Title,
                                          request.Description,
                                          request.IdClient,
                                          request.IdFreela,
                                          request.TotalCost);

            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}
