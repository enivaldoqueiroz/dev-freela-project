using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly DevFreelaDbContext _dbContext;

        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            project.Update(request.Title, request.Description, request.TotalCost);

            await _dbContext.SaveChangesAsync();
        }
    }
}
