public class Weather
{
    public string forecast { get; set; }
}

public class WeatherService
{
    List<string> forecasts = new List<string>();

    public WeatherService() { }

    public List<string> getAllForecasts()
    {
        printForecasts();
        return this.forecasts;
    }

    public bool SaveForecast(string forecast)
    {
        this.forecasts.Add(forecast);
        printForecasts();
        return true;
    }

    public bool deleteForecast(int forecastIndex)
    {
        if (forecastIndex < 0 || forecastIndex >= forecasts.Count()) return false;
        forecasts.RemoveAt(forecastIndex);
        printForecasts();
        return true;
    }

    private void printForecasts()
    {
        foreach (string forecast in forecasts) Console.WriteLine(forecast);
    }
}