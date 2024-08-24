using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DevFreelaDbContext _devFreelaDbContext;

        public UnitOfWork(IProjectRepository projects, IUserRepository users, DevFreelaDbContext devFreelaDbContext, ISkillRepository skills)
        {
            Projects = projects;
            Users = users;
            _devFreelaDbContext = devFreelaDbContext;
            Skills = skills;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public ISkillRepository Skills { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _devFreelaDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveChangesAsync()
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
