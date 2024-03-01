using AuthService.Entities;
using AuthService.Models;

namespace AuthService.Service;

public interface IUserService
{
    SignInResponseModel? SignIn(SignInRequest model);

    SignUpResponseModel? SignUp(SignUpRequest model);

    SignInResponseModel? ForgotPassword(string username);

    IEnumerable<User> GetAll();

    User? GetById(Guid userId);
}