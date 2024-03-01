using Core.Db;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DB;

public class RoleDbContext : DbContext
{
    private readonly IDbSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public RoleDbContext(
        IDbSettings settings,
        ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _loggerFactory = loggerFactory;
    }

    public DbSet<RoleBlob> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_settings.RoleConnectionString);
        builder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var templateDomainModelDbos = modelBuilder.Entity<UserDbo>();
        templateDomainModelDbos.HasKey(x => x.Id);
    }
}