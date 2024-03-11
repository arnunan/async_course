using AuthService.Session;
using Microsoft.AspNetCore.Mvc;
using Template.FrontApi.Service;

namespace Template.FrontApi.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAccountService accountService, IAnalyticsService analyticsService)
    {
        _accountService = accountService;
        _analyticsService = analyticsService;
    }

    [HttpGet("today-total-cost")]
    public IActionResult GetTodayTotalCost([ModelBinder] Session session)
    {
        var taskModels = _accountService.GetTodayTotalCost();
        return Ok(taskModels);
    }

    [HttpGet("red-employees")]
    public async Task<IActionResult> GetRedEmployees([ModelBinder] Session session)
    {
        var taskModels = await _analyticsService.GetRedEmployees();
        return Ok(taskModels);
    }

    [HttpGet("most-expensive/day")]
    public async Task<IActionResult> GetMostExpensiveTaskOfDay([ModelBinder] Session session)
    {
        var taskModels = await _analyticsService.GetMostExpensiveTaskOfDay();
        return Ok(taskModels);
    }

    [HttpGet("most-expensive/week")]
    public async Task<IActionResult> GetMostExpensiveTaskOfWeek([ModelBinder] Session session)
    {
        var taskModels = await _analyticsService.GetMostExpensiveTaskOfWeek();
        return Ok(taskModels);
    }

    [HttpGet("most-expensive/month")]
    public async Task<IActionResult> GetMostExpensiveTaskOfMonth([ModelBinder] Session session)
    {
        var taskModels = await _analyticsService.GetMostExpensiveTaskOfMonth();
        return Ok(taskModels);
    }
}