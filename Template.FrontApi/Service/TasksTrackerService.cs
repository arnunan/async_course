using Core.KafkaClient;
using Template.FrontApi.DB;
using Template.FrontApi.Models;
using Template.FrontApi.Service;
using ContextSupport = Core.Db.ContextSupport;

namespace AuthService.Service;

public class TasksTrackerService : ITasksTrackerService
{
    private readonly TasksDbContext _tasksDbContext;
    private static MessageBus? _msgBus;

    public TasksTrackerService(ContextSupport.IDbContextFactory<TasksDbContext> authContextFactory)
    {
        _msgBus = new MessageBus("localhost");
        _tasksDbContext = authContextFactory.CreateDbContext();
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
            Cost = 0
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
            Cost = 0
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
            CreatedAt = taskModel.CreatedAt
        };
        _tasksDbContext.Add(taskDbo);
        _msgBus.SendMessage("created-task", taskDbo.Id.ToString());
    }

    public void AssignTasks()
    {
        throw new NotImplementedException();
    }
}