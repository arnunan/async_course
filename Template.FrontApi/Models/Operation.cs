namespace Template.FrontApi.Models;

public class Operation
{
    public Guid Id { get; set; }

    public DateTime TimeInUtc { get; set; }

    public Guid TaskId { get; set; }

    public OperationStatus Status { get; set; }

    public decimal Count { get; set; }
}