namespace Billing.Core.Services;

public interface IAccountService
{
    void CreateAccount(Guid userId);

    void UpdateAccount(Guid userId, int amount);

    void ResetAccounts();
    
    int GetBalance(Guid userId);
    
    int GetNegativeBalanceCount();
}