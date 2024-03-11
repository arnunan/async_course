using Core.KafkaClient;
using Template.FrontApi.Clients;
using Template.FrontApi.DB;
using Template.FrontApi.Models;
using ContextSupport = Core.Db.ContextSupport;
using TaskStatus = Template.FrontApi.DB.TaskStatus;

namespace Template.FrontApi.Service;

public class TasksTrackerService : ITasksTrackerService
{
    private readonly TasksDbContext _tasksDbContext;
    private readonly AuthApiClient _authApiClient;
    private static MessageBus? _msgBus;

    public TasksTrackerService(ContextSupport.IDbContextFactory<TasksDbContext> taskDbContextFactory,
        AuthApiClient authApiClient)
    {
        _authApiClient = authApiClient;
        _msgBus = new MessageBus();
        _tasksDbContext = taskDbContextFactory.CreateDbContext();
    }

    public TaskModel[] GetTasks(Guid userId)
    {
        var tasks = _tasksDbContext.Tasks.Where(t => t.Assigned == userId).ToArray();

        return tasks.Select(t => new TaskModel
        {
            Id = t.Id,
            Assigned = t.Assigned,
            Topic = t.Topic,
            Content = t.Content,
            CreatedAt = t.CreatedAt,
            Cost = t.Cost
        }).ToArray();
    }

    public TaskModel[] GetAllTasks()
    {
        var tasks = _tasksDbContext.Tasks;

        return tasks.Select(t => new TaskModel
        {
            Id = t.Id,
            Assigned = t.Assigned,
            Topic = t.Topic,
            Content = t.Content,
            CreatedAt = t.CreatedAt,
            Cost = t.Cost
        }).ToArray();
    }

    public void CreateTask(TaskModel taskModel)
    {
        var taskDbo = new TaskDbo
        {
            Id = Guid.NewGuid(),
            Assigned = taskModel.Assigned,
            Topic = taskModel.Topic,
            Content = taskModel.Content,
            CreatedAt = taskModel.CreatedAt,
        };
        _tasksDbContext.Add(taskDbo);
        _msgBus?.SendMessage("created-task", new MessageContract(taskDbo.Id));
    }

    public async Task AssignTasks()
    {
        var tasks = _tasksDbContext.Tasks;
        var users = await _authApiClient.GetUsersForAssign();
        if (users == null)
            return;
        var random = new Random();
        foreach (var task in tasks)
            task.Assigned = users[random.Next(0, users.Length)].Id;

        await _tasksDbContext.AddRangeAsync(tasks);
        await _tasksDbContext.SaveChangesAsync();

        foreach (var task in tasks)
            _msgBus?.SendMessage("assigned-task", new MessageContract(task.Id));
    }

    public void FinishTask(Guid taskId)
    {
        var task = _tasksDbContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
            return;
        task.Status = TaskStatus.Done;

        _msgBus?.SendMessage("done-task", new MessageContract(task.Id));
    }
}