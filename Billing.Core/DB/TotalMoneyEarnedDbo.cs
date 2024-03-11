using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.DB;

[Table("TotalMoneyEarned")]
public class TotalMoneyEarnedDbo
{
    [Column("date")] public DateTime Date { get; set; }

    [Column("amount")] public int Amount { get; set; }
}