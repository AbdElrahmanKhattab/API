using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace E_Commerce.API.Filters;

public class CachedAttribute : TypeFilterAttribute
{
    public CachedAttribute(int timeToLiveSeconds)
        : base(typeof(CachedFilter))
    {
        Arguments = [timeToLiveSeconds];
    }
}

public class CachedFilter : IAsyncActionFilter
{
    private readonly IDistributedCache _cache;
    private readonly int _timeToLiveSeconds;

    public CachedFilter(IDistributedCache cache, int timeToLiveSeconds)
    {
        _cache = cache;
        _timeToLiveSeconds = timeToLiveSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!HttpMethods.IsGet(context.HttpContext.Request.Method))
        {
            await next();
            return;
        }

        var cacheKey = GenerateCacheKey(context.HttpContext.Request);
        var cachedResponse = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(cachedResponse))
        {
            context.Result = new ContentResult
            {
                Content = cachedResponse,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(okObjectResult.Value);
            await _cache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_timeToLiveSeconds)
            });
        }
    }

    private static string GenerateCacheKey(HttpRequest request)
    {
        var keyBuilder = new StringBuilder(request.Path);

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append('|').Append(key).Append('-').Append(value);
        }

        return keyBuilder.ToString();
    }
}
