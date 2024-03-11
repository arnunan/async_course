using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.DB;

[Table("Task")]
public class TaskDbo
{
    [Column("prefixId")] public string PrefixId { get; set; }

    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Column("assigned")] public Guid Assigned { get; set; }

    [Column("topic")] public string Topic { get; set; }

    [Column("content")] public string Content { get; set; }

    [Column("status")] public TaskStatus Status { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }
    
    [Column("cost")] public int Cost { get; set; }
}