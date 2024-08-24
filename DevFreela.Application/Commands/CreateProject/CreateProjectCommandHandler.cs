using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = new Project(request.Title,
                                          request.Description,
                                          request.IdClient,
                                          request.IdFreela,
                                          request.TotalCost);

            project.Comments.Add(new ProjectComment("Project was created.", project.Id, request.IdClient));

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.Skills.AddSkillFromProjectAsync(project);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            return project.Id;
        }
    }
}
