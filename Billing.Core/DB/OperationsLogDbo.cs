using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.DB;

[Table("OperationsLog")]
public class OperationsLogDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Column("timeInUtc")] public DateTime TimeInUtc { get; set; }

    [Column("userId")] public Guid UserId { get; set; }

    [Column("taskId")] public Guid TaskId { get; set; }

    [Column("status")] public int Status { get; set; }

    [Column("amount")] public int Amount { get; set; }
}