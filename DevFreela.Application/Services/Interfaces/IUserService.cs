using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll(string query);
        UserViewModel GetUser(int id);
        int Create(CreateUserInputModel inputModel);
        void Delete(int id);
    }
}
