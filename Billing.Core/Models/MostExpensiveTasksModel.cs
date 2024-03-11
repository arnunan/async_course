namespace Billing.Core.Models;

public class MostExpensiveTasksModel
{
    public DateTime Date { get; set; }

    public string PrefixId { get; set; }

    public Guid Id { get; set; }

    public int Cost { get; set; }
}