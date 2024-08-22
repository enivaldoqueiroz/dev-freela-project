using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Project> paginationProjects = await _projectRepository.GetAllAsync(request.Query, request.Page);

            var projectsViewModel = paginationProjects
                .Data
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt))
                .ToList();

            var paginationProjectViewModel = new PaginationResult<ProjectViewModel>(
                paginationProjects.Page,
                paginationProjects.TotalPages,
                paginationProjects.PageSize,
                paginationProjects.ItensCount,
                projectsViewModel
            );

            return paginationProjectViewModel;
        }
    }
}
