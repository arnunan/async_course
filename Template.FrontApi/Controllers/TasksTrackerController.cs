using Microsoft.AspNetCore.Mvc;
using Template.FrontApi.Models;
using Template.FrontApi.Service;

namespace Template.FrontApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksTrackerController : ControllerBase
{
    private readonly ITasksTrackerService _tasksTrackerService;

    public TasksTrackerController(ITasksTrackerService tasksTrackerService)
    {
        this._tasksTrackerService = tasksTrackerService;
    }

    [HttpGet]
    public IActionResult GetTasks(Guid userId)
    {
        var taskModel = _tasksTrackerService.GetTasks(userId);
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
}