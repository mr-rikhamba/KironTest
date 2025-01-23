using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace KironTest;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SimpleCacheAttribute : ActionFilterAttribute
{
    private readonly int _ttlMins;
    private readonly IMemoryCache _memCache;

    public SimpleCacheAttribute(int ttlMins = 5)
    {
        _ttlMins = ttlMins;
        _memCache = new MemoryCache(new MemoryCacheOptions());
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var key = GenerateCacheKey(context);
        if (_memCache.TryGetValue(key, out var cachedData))
        {
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(cachedData);
            return;
        }

        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var key = GenerateCacheKey(context);

        if (context.Result is ObjectResult objectResult && objectResult.StatusCode == 200)
        {
            _memCache.Set(key, objectResult.Value, DateTime.Now.AddMinutes(_ttlMins));
        }

        base.OnActionExecuted(context);
    }

    private string GenerateCacheKey(FilterContext context)
    {
        var key = context.HttpContext.Request.Path.ToString();
        var queryString = context.HttpContext.Request.QueryString.ToString();
        return $"{key}{queryString}";
    }
}
