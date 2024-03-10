using AuthService.Session;
using Microsoft.AspNetCore.Mvc;
using Template.FrontApi.Service;

namespace Template.FrontApi.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService) =>
        _accountService = accountService;

    [HttpGet("operation-log")]
    public async Task<IActionResult> GetOperationLog([ModelBinder] Session session)
    {
        var operationLog = await _accountService.GetOperationLog(session.UserId);
        return Ok(operationLog);
    }

    [HttpGet("current-balance")]
    public async Task<IActionResult> GetCurrentBalance([ModelBinder] Session session)
    {
        var taskModels = await _accountService.GetCurrentBalance(session.UserId);
        return Ok(taskModels);
    }

    [HttpGet("today-total-cost")]
    public IActionResult GetTodayTotalCost([ModelBinder] Session session)
    {
        var taskModels = _accountService.GetTodayTotalCost();
        return Ok(taskModels);
    }
}