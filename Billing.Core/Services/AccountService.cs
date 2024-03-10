using Billing.Core.DB;
using ContextSupport = Core.Db.ContextSupport;

namespace Billing.Core.Services;

public class AccountService : IAccountService
{
    private readonly AccountDbContext _accountDbContext;

    public AccountService(ContextSupport.IDbContextFactory<AccountDbContext> accountContextFactory) =>
        _accountDbContext = accountContextFactory.CreateDbContext();

    public void CreateAccount(Guid userId)
    {
        _accountDbContext.Add(new AccountDbo
        {
            UserId = userId,
            Amount = 0
        });
    }

    public void UpdateAccount(Guid userId, int amount)
    {
        var account = _accountDbContext.Accounts.FirstOrDefault(a => a.UserId == userId);
        if (account == null)
            return;
        account.Amount += amount;
        _accountDbContext.Add(account);
    }

    public void ResetAccounts()
    {
        var accounts = _accountDbContext.Accounts;
        foreach (var account in accounts)
            account.Amount = 0;
        _accountDbContext.AddRangeAsync(accounts);
        _accountDbContext.SaveChangesAsync();
    }

    public int GetBalance(Guid userId)
    {
        return _accountDbContext.Accounts.FirstOrDefault(a => a.UserId == userId)?.Amount ?? 0;
    }

    public int GetNegativeBalanceCount()
    {
        return _accountDbContext.Accounts.Count(a => a.Amount < 0);
    }
}