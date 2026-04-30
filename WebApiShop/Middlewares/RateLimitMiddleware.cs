using Microsoft.Extensions.Caching.Memory;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

    public RateLimitMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var cacheKey = $"ratelimit_{ip}";
        if (!_cache.TryGetValue(cacheKey, out int count))
        {
            _cache.Set(cacheKey, 1, TimeSpan.FromSeconds(60));
        }
        else if (count >= 100) 
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Too many requests, slow down!");
            return;
        }
        else
        {
            _cache.Set(cacheKey, count + 1, TimeSpan.FromSeconds(60));
        }

        await _next(context); 
    }
}