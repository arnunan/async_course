using Template.FrontApi.Models;

namespace Template.FrontApi.Service;

public interface ITasksTrackerService
{
    TaskModel[] GetTasks(Guid userId);
    
    TaskModel[] GetAllTasks();
    
    void CreateTask(TaskModel taskModel);
    
    Task AssignTasks();
    
    void FinishTask(Guid taskId);
}