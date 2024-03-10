namespace Billing.Core.Models;

public class TaskModel
{
    public string PrefixId { get; set; }
    
    public Guid Id { get; set; }

    public Guid Assigned { get; set; }

    public string Topic { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public int Cost { get; set; }
}