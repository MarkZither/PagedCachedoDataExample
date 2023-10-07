namespace PagedCached.oData;

public class WeatherForecast
{
    //[Key]
    // don't need key attribute if the column is Id
    public int Id {get;set;}
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
