namespace Billing.Core.Models;

public class Operation
{
    public Guid Id { get; set; }

    public DateTime TimeInUtc { get; set; }

    public Guid UserId { get; set; }

    public Guid TaskId { get; set; }

    public OperationStatus Status { get; set; }

    public int Amount { get; set; }
}