public class Weather
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Forecast { get; set; }
}
GCNotificationStatus
public class WeatherService
{
    private readonly List<Weather> forecasts = new();

    public List<Weather> GetAllForecasts() => forecasts;

    public void SaveForecast(Weather forecast)
    {
        forecasts.Add(forecast);
        PrintForecasts();
    }

    public bool DeleteForecast(DateTime date)
    {
        var forecast = forecasts.FirstOrDefault(f => f.Date.Date == date.Date);
        if (forecast == null) return false;

        forecasts.Remove(forecast);
        PrintForecasts();
        return true;
    }

    private void PrintForecasts()
    {
        foreach (var f in forecasts)
        {
            Console.WriteLine($"{f.Date:yyyy-MM-dd}: {f.Forecast}");
        }
    }
}