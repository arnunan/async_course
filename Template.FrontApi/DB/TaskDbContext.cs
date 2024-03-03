using Core.Db;
using Microsoft.EntityFrameworkCore;

namespace Template.FrontApi.DB;

public class TasksDbContext : DbContext
{
    private readonly IDbSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public TasksDbContext(
        IDbSettings settings,
        ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _loggerFactory = loggerFactory;
    }

    public DbSet<TaskDbo> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_settings.ConnectionString);
        builder.UseLoggerFactory(_loggerFactory);
    }

    public void Add(TaskDbo task)
    {
        Tasks.Add(task);
        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userDbo = modelBuilder.Entity<TaskDbo>();
        userDbo.HasKey(x => x.Id);
    }
}