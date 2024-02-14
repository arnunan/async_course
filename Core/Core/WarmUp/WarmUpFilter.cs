using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Core.WarmUp;

public class WarmUpFilter : IStartupFilter
{
    private readonly IEnumerable<IWarmUp> _warmUps;

    public WarmUpFilter(IEnumerable<IWarmUp> warmUps)
    {
        _warmUps = warmUps;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        foreach (var warmUp in _warmUps)
        {
            warmUp.RunAsync().GetAwaiter().GetResult();
        }
   
        return next;
    }
}