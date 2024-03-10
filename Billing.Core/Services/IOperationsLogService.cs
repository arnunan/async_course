using Billing.Core.Models;

namespace Billing.Core.Services;

public interface IOperationsLogService
{
    void AddOperations(Operation operation);

    Operation[] GetOperations(DateTime dateStart, DateTime dateEnd);
    
    Operation[] GetOperations(Guid userId);
}