using AuthService.Entities;
using AuthService.Models;

namespace AuthService.Service;

public interface IUserService
{
    User? SignIn(SignInRequest model);

    User SignUp(SignUpRequest model);

    SignInResponseModel? ForgotPassword(string username);

    IEnumerable<User> GetAll();

    IEnumerable<User> GetAllForAssign();

    User? GetById(Guid userId);
}