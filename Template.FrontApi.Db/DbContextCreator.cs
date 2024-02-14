using Core.Db;
using Core.Db.ContextSupport;
using Microsoft.Extensions.Logging;

namespace Template.FrontApi.Db;

public class TemplateApiDbContextCreator : IDbContextCreator<TemplateApiDbContext>
{
    public TemplateApiDbContext Create(IDbSettings settings, ILoggerFactory loggerFactory)
    {
        return new TemplateApiDbContext(settings, loggerFactory);
    }
}