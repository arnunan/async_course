using Template.FrontApi.Clients;

namespace Template.FrontApi.Service;

public class AnalyticsService : IAnalyticsService
{
    private readonly BillingApiClient _billingApiClient;

    public AnalyticsService(BillingApiClient billingApiClient)
    {
        _billingApiClient = billingApiClient;
    }

    public async Task<int?> GetRedEmployees()
    {
        return await _billingApiClient.GetRedEmployees();
    }

    public async Task<Guid?> GetMostExpensiveTaskOfDay()
    {
        return await _billingApiClient.GetMostExpensiveTaskOfDay();
    }

    public async Task<Guid?> GetMostExpensiveTaskOfWeek()
    {
        return await _billingApiClient.GetMostExpensiveTaskOfWeek();
    }

    public async Task<Guid?> GetMostExpensiveTaskOfMonth()
    {
        return await _billingApiClient.GetMostExpensiveTaskOfMonth();
    }
}