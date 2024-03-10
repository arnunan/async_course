namespace Template.FrontApi.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string Login { get; set; }

    public string Role { get; set; }
}