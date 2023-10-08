using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PagedCached.oData.Controllers;

[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class oDataWeatherForecastController : ODataController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<oDataWeatherForecastController> _logger;

    public oDataWeatherForecastController(ILogger<oDataWeatherForecastController> logger)
    {
        _logger = logger;
    }

    [EnableQuery(PageSize = 2)]
    [HttpGet(Name = "GetoDataWeatherForecast")]
    public IEnumerable<oDataWeatherForecast> Get()
    {
        return Enumerable.Range(1, 50).Select(index => new oDataWeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
