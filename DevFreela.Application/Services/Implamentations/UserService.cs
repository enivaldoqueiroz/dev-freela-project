using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implamentations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateUserInputModel inputModel)
        {
            User user = new User(inputModel.FullName,
                                 inputModel.Email,
                                 inputModel.BirthDate);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public UserViewModel GetUser(int id)
        {
            User user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return null;
            }

            return new UserViewModel(user.FullName, user.Email);
        }

        public void Delete(int id)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            user.Delete();

            _dbContext.SaveChanges();
        }

        public List<UserViewModel> GetAll(string query)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
