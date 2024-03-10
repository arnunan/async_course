using System.ComponentModel.DataAnnotations.Schema;

namespace Template.FrontApi.DB;

[Table("Task")]
public class TaskDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Column("assigned")] public Guid Assigned { get; set; }

    [Column("topic")] public string Topic { get; set; }

    [Column("content")] public string Content { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }
}