using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.DB;

[Table("Role")]
public class RoleDbo
{
    [Column("id")]
    [Key]
    public int RoleId { get; set; }

    [Column("rolename")]
    public string RoleName { get; set; }
}