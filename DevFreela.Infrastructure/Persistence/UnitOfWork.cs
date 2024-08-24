using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _devFreelaDbContext;

        public UnitOfWork(IProjectRepository projects, IUserRepository users, DevFreelaDbContext devFreelaDbContext)
        {
            Projects = projects;
            Users = users;
            _devFreelaDbContext = devFreelaDbContext;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public async Task<int> CompleteAsync()
        {
           return await _devFreelaDbContext.SaveChangesAsync();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _devFreelaDbContext.Dispose();
            }
        }
    }
}
