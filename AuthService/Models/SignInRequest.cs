using System.ComponentModel.DataAnnotations;

namespace AuthService.Models;

public class SignInRequest
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}