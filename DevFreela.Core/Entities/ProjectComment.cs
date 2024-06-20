namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int idProject, int idUser, DateTime createAt)
        {
            Content = content;
            IdProject = idProject;
            IdUser = idUser;
            CreateAt = createAt;
        }

        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public int IdUser { get; private set; }
        public DateTime CreateAt { get; private set; }
    }
}
