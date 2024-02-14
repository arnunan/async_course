using Core.Db.ContextSupport;

namespace Template.FrontApi.Db;

public class DesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<TemplateApiDbContext, TemplateApiDbContextCreator>
{
    protected override string MigrationDatabaseName { get; } = "Test";
}