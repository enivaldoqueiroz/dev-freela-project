using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public CreateCommentCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            ProjectComment projectComment = new ProjectComment(request.Content,
                                                               request.IdProject,
                                                               request.IdUser);

            await _dbContext.ProjectComments.AddAsync(projectComment);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
