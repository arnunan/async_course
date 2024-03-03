namespace Template.FrontApi.Models;

public class TaskModel
{
    public Guid Id { get; set; }

    public Guid Assigned { get; set; }

    public string Topic { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public decimal Cost { get; set; }
}