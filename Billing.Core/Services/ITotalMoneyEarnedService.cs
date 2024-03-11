using Billing.Core.Models;

namespace Billing.Core.Services;

public interface ITotalMoneyEarnedService
{
    void AddTotalMoneyEarned(TotalMoneyEarned totalMoneyEarned);

    int GetTotalMoneyEarned(DateTime date);
}