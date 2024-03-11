using Template.FrontApi.Models;

namespace Template.FrontApi.Service;

public interface IAccountService
{
    Task<Operation[]?> GetOperationLog(Guid userId);

    Task<decimal> GetCurrentBalance(Guid userId);

    int GetTodayTotalCost();
}