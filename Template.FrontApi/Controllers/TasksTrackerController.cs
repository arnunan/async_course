using AuthService.Session;
using Microsoft.AspNetCore.Mvc;
using Template.FrontApi.Models;
using Template.FrontApi.Service;

namespace Template.FrontApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksTrackerController : ControllerBase
{
    private readonly ITasksTrackerService _tasksTrackerService;

    public TasksTrackerController(ITasksTrackerService tasksTrackerService) =>
        _tasksTrackerService = tasksTrackerService;

    [HttpGet]
    public IActionResult GetTasks([ModelBinder] Session session)
    {
        var taskModel = _tasksTrackerService.GetTasks(session.UserId);
        return Ok(taskModel);
    }

    [HttpGet("all-tasks")]
    public IActionResult GetAllTasks()
    {
        var taskModels = _tasksTrackerService.GetAllTasks();
        return Ok(taskModels);
    }

    [HttpPost("create")]
    public IActionResult CreateTask(TaskModel task)
    {
        _tasksTrackerService.CreateTask(task);
        return Ok();
    }

    [HttpPost("assign")]
    public IActionResult AssignTasks()
    {
        _tasksTrackerService.AssignTasks();
        return Ok();
    }

    [HttpPost("finish")]
    public IActionResult FinishTask(Guid taskId)
    {
        _tasksTrackerService.FinishTask(taskId);
        return Ok();
    }
}