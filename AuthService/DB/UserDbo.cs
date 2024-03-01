using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.DB;

[Table("auth")]
public class UserDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Column("login")] public string Login { get; set; }

    [Column("password")] public string Password { get; set; }

    [Column("firstName")] public string FirstName { get; set; }

    [Column("secondName")] public string SecondName { get; set; }

    [Column("roleId")] public int RoleId { get; set; }
}