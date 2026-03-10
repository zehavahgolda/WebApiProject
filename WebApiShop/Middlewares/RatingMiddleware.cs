using Entity;
using Microsoft.AspNetCore.Http;
using Repository;
using System;
using System.Threading.Tasks;

public class RatingMiddleware
{
    private readonly RequestDelegate _next;

    public RatingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IRatingService ratingService)
    {
        await _next(context);

        var rating = new Rating
        {
            Host = context.Request.Host.ToString(),
            Method = context.Request.Method,
            Path = context.Request.Path,
            Referer = context.Request.Headers["Referer"].ToString(),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            Record_Date = DateTime.Now
        };

        await ratingService.AddRatingAsync(rating);
    }

}
