using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implamentations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<SkillViewModel> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string script = "SELECT Id, Description FROM Skills";

                return sqlConnection.Query<SkillViewModel>(script).ToList();
            }

            //List<Skill> skills = _dbContext.Skills.ToList();

            //List<SkillViewModel> skillViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();

            //return skillViewModel;
        }
    }
}
