using System.Text.Json.Serialization;

namespace AuthService.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string Login { get; set; }

    [JsonIgnore] public string Password { get; set; }

    public string Role { get; set; }
}