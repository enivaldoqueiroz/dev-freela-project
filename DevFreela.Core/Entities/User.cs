namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User() { }

        public User(string fullName, string email, DateTime birthDate, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            Password = password;
            Role = role;

            CreateAt = DateTime.UtcNow;
            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreateAt { get; private set; }
        public bool Active { get; set; }

        public string Password { get; private set; }
        public string Role { get; private set; }

        public List<UserSkill> Skills { get; private set;}
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; set; }
        public List<ProjectComment> Comments { get; set; }

        public void Delete()
        {
            if (Active)
                _ = Active == false;
        }
    }
}
