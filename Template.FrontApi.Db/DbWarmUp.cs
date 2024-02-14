using Core.Db;
using Core.Db.ContextSupport;

namespace Template.FrontApi.Db;

public class TemplateApiDbWarmUp : DbWarmUpBase<TemplateApiDbContext>
{
    public TemplateApiDbWarmUp(
        IDbContextFactory<TemplateApiDbContext> dbContextFactory,
        IDbSettings settings)
        : base(dbContextFactory, settings)
    {
    }
}