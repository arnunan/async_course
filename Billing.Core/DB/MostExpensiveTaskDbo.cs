using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.DB;

[Table("MostExpensiveTask")]
public class MostExpensiveTaskDbo
{
    [Column("date")] public DateTime Date { get; set; }

    [Column("prefixId")] public string PrefixId { get; set; }

    [Column("id")] public Guid Id { get; set; }

    [Column("cost")] public int Cost { get; set; }
}