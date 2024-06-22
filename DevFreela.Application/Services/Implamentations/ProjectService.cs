using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implamentations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {
            Project project = new Project(inputModel.Title, 
                                          inputModel.Description, 
                                          inputModel.IdClient, 
                                          inputModel.IdFreela, 
                                          inputModel.TotalCost);

            _dbContext.Projects.Add(project);

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            ProjectComment projectComment = new ProjectComment(inputModel.Content, 
                                                               inputModel.IdProject, 
                                                               inputModel.IdUser);

            _dbContext.ProjectComments.Add(projectComment);
        }

        public void Delete(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Cancel();
        }

        public void Finish(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            List<Project> projects = _dbContext.Projects.ToList();

            List<ProjectViewModel> projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            Project project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

            if (project is null) return null;

            ProjectDetailsViewModel projectDetailsViewModel = new ProjectDetailsViewModel(project.Id,
                                                                                          project.Title, 
                                                                                          project.Description,
                                                                                          project.TotalCost,
                                                                                          project.StartedAt,
                                                                                          project.FinishedAt
                                                                                         );

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();
        }

        public int Update(UpdateProjectInputModel inputModel)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            return project.Id;
        }
    }
}
