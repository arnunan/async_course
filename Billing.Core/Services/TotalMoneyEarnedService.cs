using Billing.Core.DB;
using Billing.Core.Models;
using ContextSupport = Core.Db.ContextSupport;

namespace Billing.Core.Services;

public class TotalMoneyEarnedService : ITotalMoneyEarnedService
{
    private readonly TotalMoneyEarnedDbContext _totalMoneyEarnedDbContext;

    public TotalMoneyEarnedService(
        ContextSupport.IDbContextFactory<TotalMoneyEarnedDbContext> totalMoneyEarnedDbFactory) =>
        _totalMoneyEarnedDbContext = totalMoneyEarnedDbFactory.CreateDbContext();


    public void AddTotalMoneyEarned(TotalMoneyEarned totalMoneyEarned)
    {
        _totalMoneyEarnedDbContext.Add(new TotalMoneyEarnedDbo
        {
            Date = totalMoneyEarned.Date,
            Amount = totalMoneyEarned.Amount
        });
    }

    public int GetTotalMoneyEarned(DateTime date)
    {
        return _totalMoneyEarnedDbContext.TotalMoneyEarneds
            .FirstOrDefault(t => t.Date == date)?.Amount ?? 0;
    }
}