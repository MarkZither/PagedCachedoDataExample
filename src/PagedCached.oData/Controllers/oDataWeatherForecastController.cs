using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using LazyCache;

namespace PagedCached.oData.Controllers;

[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class oDataWeatherForecastController : ControllerBase
{
    private readonly IAppCache _cache;
    private readonly ILogger<oDataWeatherForecastController> _logger;
    private readonly string cacheKey = "oDataWeatherForecastController.Get";
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public oDataWeatherForecastController(ILogger<oDataWeatherForecastController> logger, IAppCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [EnableQuery(PageSize = 2)]
    [HttpGet(Name = "GetoDataWeatherForecast")]
    public IEnumerable<oDataWeatherForecast> Get()
    {
        Func<IEnumerable<oDataWeatherForecast>> actionThatWeWantToCache = () => GetForecasts();

        var cachedDatabaseTime = _cache.GetOrAdd(cacheKey, actionThatWeWantToCache);   
        
        return cachedDatabaseTime;
    }

    private IEnumerable<oDataWeatherForecast> GetForecasts()
    {
        return Enumerable.Range(1, 1_000_000).Select(index => new oDataWeatherForecast
                {
                    Id = index,
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();
    }
}
