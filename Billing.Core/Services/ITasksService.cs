using Billing.Core.Models;

namespace Billing.Core.Services;

public interface ITasksService
{
    TaskModel[] GetTasks(DateTime date);
}