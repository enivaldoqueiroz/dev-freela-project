using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implamentations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public void Finish(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();

            _dbContext.SaveChanges();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            List<Project> projects = _dbContext.Projects.ToList();

            List<ProjectViewModel> projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            Project project = _dbContext.Projects
                                        .Include(p => p.Client)
                                        .Include(p => p.Freelancer)
                                        .SingleOrDefault(p => p.Id == id);

            if (project is null) return null;

            ProjectDetailsViewModel projectDetailsViewModel = new ProjectDetailsViewModel(project.Id,
                                                                                          project.Title, 
                                                                                          project.Description,
                                                                                          project.TotalCost,
                                                                                          project.StartedAt,
                                                                                          project.FinishedAt,
                                                                                          project.Client.FullName,
                                                                                          project.Freelancer.FullName
                                                                                         );

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();

            //_dbContext.SaveChanges();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString)) 
            { 
                sqlConnection.Open();

                string script = "UPDATE Projects " +
                                "SET Status = @status, StartedAt = @startedat" +
                                "WHERE Id = @id";

                sqlConnection.Execute(script, new { status = project.Status, 
                                                    startedat = project.StartedAt, 
                                                    id});
            }
        }
    }
}
