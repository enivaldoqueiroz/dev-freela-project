using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Minha Descrição de Projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha Descrição de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha Descrição de Projeto 3", 1, 1, 30000)
            };

            Users = new List<User>
            {
                new User("Luis Felipe", "luisfelipe@luisdev.com.br", new DateTime(1992,1,1)),
                new User("Robert C Martin", "robert@luisdev.com.br", new DateTime(1975,1,1)),
                new User("Enivaldo", "enivaldo@luisdev.com.br", new DateTime(19791,1,1)),
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("SQL"),
            };
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
