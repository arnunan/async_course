using AuthService.Entities;

namespace AuthService.Models;

public class SignInResponseModel
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string Role { get; set; }

    public string Token { get; set; }


    public SignInResponseModel(User user, string token)
    {
        Id = user.Id;
        Username = user.Login;
        FirstName = user.FirstName;
        SecondName = user.SecondName;
        Role = user.Role;
        Token = token;
    }
}