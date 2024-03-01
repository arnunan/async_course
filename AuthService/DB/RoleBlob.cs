using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.DB;

[Table("role")]
public class RoleBlob
{
    [Column("roleId")] public int RoleId { get; set; }

    [Column("roleName")] public string RoleName { get; set; }
}