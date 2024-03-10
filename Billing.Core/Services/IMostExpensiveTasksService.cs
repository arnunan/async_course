using Billing.Core.Models;

namespace Billing.Core.Services;

public interface IMostExpensiveTasksService
{
    MostExpensiveTasksModel Get(DateTime date);

    MostExpensiveTasksModel[] Get(DateTime dateStart, DateTime dateEnd);
    
    void Add(MostExpensiveTasksModel mostExpensiveTasksModel);
}