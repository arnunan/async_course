namespace Template.FrontApi.Service;

public interface IAnalyticsService
{
    Task<int?> GetRedEmployees();

    Task<Guid?> GetMostExpensiveTaskOfDay();

    Task<Guid?> GetMostExpensiveTaskOfWeek();

    Task<Guid?> GetMostExpensiveTaskOfMonth();
}