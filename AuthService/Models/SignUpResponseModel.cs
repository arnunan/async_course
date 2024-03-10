using AuthService.Entities;

namespace AuthService.Models;

public class SignUpResponseModel
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public SignUpResponseModel(User user)
    {
        Id = user.Id;
        Username = user.Login;
    }
}