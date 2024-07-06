using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
