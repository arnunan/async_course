using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.DB;

[Table("Account")]
public class AccountDbo
{
    [Column("userid")]
    [Key]
    public Guid UserId { get; set; }

    [Column("amount")]
    public int Amount { get; set; }
}