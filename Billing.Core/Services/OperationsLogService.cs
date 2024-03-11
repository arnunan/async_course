using Billing.Core.DB;
using Billing.Core.Models;
using ContextSupport = Core.Db.ContextSupport;
using OperationStatus = Billing.Core.Models.OperationStatus;

namespace Billing.Core.Services;

public class OperationsLogService : IOperationsLogService
{
    private readonly OperationsLogDbContext _operationsLogDbContext;

    public OperationsLogService(ContextSupport.IDbContextFactory<OperationsLogDbContext> operationsLogContextFactory) =>
        _operationsLogDbContext = operationsLogContextFactory.CreateDbContext();

    public void AddOperations(Operation operation)
    {
        _operationsLogDbContext.Add(new OperationsLogDbo
        {
            Id = Guid.NewGuid(),
            TimeInUtc = DateTime.Now.ToUniversalTime(),
            UserId = operation.UserId,
            TaskId = operation.TaskId,
            Status = (int)operation.Status,
            Amount = operation.Amount
        });
    }

    public Operation[] GetOperations(DateTime dateStart, DateTime dateEnd)
    {
        var operations = _operationsLogDbContext.OperationsLog.Where(o => o.TimeInUtc > dateStart && o.TimeInUtc < dateEnd).ToArray();
        return operations.Select(o => new Operation
        {
            Id = o.Id,
            TimeInUtc = o.TimeInUtc,
            UserId = o.UserId,
            TaskId = o.TaskId,
            Status = (OperationStatus)o.Status,
            Amount = o.Amount
        }).ToArray();
    }
    
    public Operation[] GetOperations(Guid userId)
    {
        var operations = _operationsLogDbContext.OperationsLog.Where(o => o.UserId == userId).ToArray();
        return operations.Select(o => new Operation
        {
            Id = o.Id,
            TimeInUtc = o.TimeInUtc,
            UserId = o.UserId,
            TaskId = o.TaskId,
            Status = (OperationStatus)o.Status,
            Amount = o.Amount
        }).ToArray();
    }
}