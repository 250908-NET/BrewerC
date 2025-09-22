public class TemperatureService
{
    private readonly ILogger<TemperatureService> _logger;

    public TemperatureService(ILogger<TemperatureService> logger)
    {
        _logger = logger;
    }

    public IResult CelsiusToFahrenheit(double celsius)
    {
        try
        {
            // Check for absolute zero violation
            if (celsius < -273.15)
            {
                return Results.BadRequest("Temperature cannot be below absolute zero (-273.15°C)");
            }

            var fahrenheit = (celsius * 9.0 / 5.0) + 32.0;
            
            _logger.LogInformation("Temperature conversion: {Celsius}°C -> {Fahrenheit}°F", celsius, fahrenheit);

            return Results.Ok(new TemperatureResponse
            {
                InputValue = celsius,
                InputUnit = "Celsius",
                OutputValue = Math.Round(fahrenheit, 2),
                OutputUnit = "Fahrenheit",
                Formula = "F = (C × 9/5) + 32"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting Celsius to Fahrenheit");
            return Results.BadRequest("Error processing temperature conversion");
        }
    }

    public IResult FahrenheitToCelsius(double fahrenheit)
    {
        try
        {
            // Check for absolute zero violation
            if (fahrenheit < -459.67)
            {
                return Results.BadRequest("Temperature cannot be below absolute zero (-459.67°F)");
            }

            var celsius = (fahrenheit - 32.0) * 5.0 / 9.0;
            
            _logger.LogInformation("Temperature conversion: {Fahrenheit}°F -> {Celsius}°C", fahrenheit, celsius);

            return Results.Ok(new TemperatureResponse
            {
                InputValue = fahrenheit,
                InputUnit = "Fahrenheit",
                OutputValue = Math.Round(celsius, 2),
                OutputUnit = "Celsius",
                Formula = "C = (F - 32) × 5/9"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting Fahrenheit to Celsius");
            return Results.BadRequest("Error processing temperature conversion");
        }
    }

    public IResult KelvinToCelsius(double kelvin)
    {
        try
        {
            // Check for absolute zero violation
            if (kelvin < 0)
            {
                return Results.BadRequest("Kelvin temperature cannot be below 0K (absolute zero)");
            }

            var celsius = kelvin - 273.15;
            
            _logger.LogInformation("Temperature conversion: {Kelvin}K -> {Celsius}°C", kelvin, celsius);

            return Results.Ok(new TemperatureResponse
            {
                InputValue = kelvin,
                InputUnit = "Kelvin",
                OutputValue = Math.Round(celsius, 2),
                OutputUnit = "Celsius",
                Formula = "C = K - 273.15"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting Kelvin to Celsius");
            return Results.BadRequest("Error processing temperature conversion");
        }
    }

    public IResult CelsiusToKelvin(double celsius)
    {
        try
        {
            // Check for absolute zero violation
            if (celsius < -273.15)
            {
                return Results.BadRequest("Temperature cannot be below absolute zero (-273.15°C)");
            }

            var kelvin = celsius + 273.15;
            
            _logger.LogInformation("Temperature conversion: {Celsius}°C -> {Kelvin}K", celsius, kelvin);

            return Results.Ok(new TemperatureResponse
            {
                InputValue = celsius,
                InputUnit = "Celsius",
                OutputValue = Math.Round(kelvin, 2),
                OutputUnit = "Kelvin",
                Formula = "K = C + 273.15"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting Celsius to Kelvin");
            return Results.BadRequest("Error processing temperature conversion");
        }
    }

    public IResult CompareTemperatures(double temp1, string unit1, double temp2, string unit2)
    {
        try
        {
            var validUnits = new[] { "celsius", "fahrenheit", "kelvin", "c", "f", "k" };
            
            unit1 = unit1.ToLower();
            unit2 = unit2.ToLower();

            if (!validUnits.Contains(unit1) || !validUnits.Contains(unit2))
            {
                return Results.BadRequest("Valid units are: celsius/c, fahrenheit/f, kelvin/k");
            }

            // Convert both temperatures to Celsius for comparison
            var temp1Celsius = ConvertToCelsius(temp1, unit1);
            var temp2Celsius = ConvertToCelsius(temp2, unit2);

            if (temp1Celsius == null || temp2Celsius == null)
            {
                return Results.BadRequest("Invalid temperature values (below absolute zero)");
            }

            var difference = Math.Abs(temp1Celsius.Value - temp2Celsius.Value);
            string comparison;
            string warmer, colder;

            if (Math.Abs(temp1Celsius.Value - temp2Celsius.Value) < 0.01) // Account for rounding
            {
                comparison = "equal";
                warmer = "Neither";
                colder = "Neither";
            }
            else if (temp1Celsius > temp2Celsius)
            {
                comparison = "warmer";
                warmer = $"{temp1}° {GetFullUnitName(unit1)}";
                colder = $"{temp2}° {GetFullUnitName(unit2)}";
            }
            else
            {
                comparison = "colder";
                warmer = $"{temp2}° {GetFullUnitName(unit2)}";
                colder = $"{temp1}° {GetFullUnitName(unit1)}";
            }

            var response = new TemperatureComparisonResponse
            {
                Temperature1 = new TemperatureValue { Value = temp1, Unit = GetFullUnitName(unit1) },
                Temperature2 = new TemperatureValue { Value = temp2, Unit = GetFullUnitName(unit2) },
                Temperature1InCelsius = Math.Round(temp1Celsius.Value, 2),
                Temperature2InCelsius = Math.Round(temp2Celsius.Value, 2),
                Comparison = comparison,
                Difference = Math.Round(difference, 2),
                WarmestTemperature = warmer,
                ColdestTemperature = colder
            };

            _logger.LogInformation("Temperature comparison: {Temp1}° {Unit1} vs {Temp2}° {Unit2} -> {Result}", 
                temp1, unit1, temp2, unit2, comparison);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error comparing temperatures");
            return Results.BadRequest("Error processing temperature comparison");
        }
    }

    // Helper methods
    private double? ConvertToCelsius(double temperature, string unit)
    {
        return unit switch
        {
            "celsius" or "c" => temperature >= -273.15 ? temperature : null,
            "fahrenheit" or "f" => temperature >= -459.67 ? (temperature - 32) * 5.0 / 9.0 : null,
            "kelvin" or "k" => temperature >= 0 ? temperature - 273.15 : null,
            _ => null
        };
    }

    private string GetFullUnitName(string unit)
    {
        return unit.ToLower() switch
        {
            "c" or "celsius" => "Celsius",
            "f" or "fahrenheit" => "Fahrenheit", 
            "k" or "kelvin" => "Kelvin",
            _ => unit
        };
    }
}

// Response Models
public record TemperatureResponse
{
    public double InputValue { get; init; }
    public string InputUnit { get; init; } = "";
    public double OutputValue { get; init; }
    public string OutputUnit { get; init; } = "";
    public string Formula { get; init; } = "";
}

public record TemperatureValue
{
    public double Value { get; init; }
    public string Unit { get; init; } = "";
}

public record TemperatureComparisonResponse
{
    public TemperatureValue Temperature1 { get; init; } = new();
    public TemperatureValue Temperature2 { get; init; } = new();
    public double Temperature1InCelsius { get; init; }
    public double Temperature2InCelsius { get; init; }
    public string Comparison { get; init; } = "";
    public double Difference { get; init; }
    public string WarmestTemperature { get; init; } = "";
    public string ColdestTemperature { get; init; } = "";
}