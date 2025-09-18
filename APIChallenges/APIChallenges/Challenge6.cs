public class Challenge6
{
    public static IResult CelsiusToFahrenheit(double temp)
    {
        var result = (temp * 9.0 / 5.0) + 32;
        return Results.Ok(new 
        { 
            operation = "celsius_to_fahrenheit",
            input = temp,
            result = Math.Round(result, 2),
            formula = "°F = (°C × 9/5) + 32"
        });
    }

    public static IResult FahrenheitToCelsius(double temp)
    {
        var result = (temp - 32) * 5.0 / 9.0;
        return Results.Ok(new 
        { 
            operation = "fahrenheit_to_celsius",
            input = temp,
            result = Math.Round(result, 2),
            formula = "°C = (°F - 32) × 5/9"
        });
    }

    public static IResult KelvinToCelsius(double temp)
    {
        if (temp < 0)
            return Results.BadRequest(new { error = "Temperature cannot be below absolute zero (0 Kelvin)" });

        var result = temp - 273.15;
        return Results.Ok(new 
        { 
            operation = "kelvin_to_celsius",
            input = temp,
            result = Math.Round(result, 2),
            formula = "°C = K - 273.15"
        });
    }

    public static IResult CelsiusToKelvin(double temp)
    {
        if (temp < -273.15)
            return Results.BadRequest(new { error = "Temperature cannot be below absolute zero (-273.15°C)" });

        var result = temp + 273.15;
        return Results.Ok(new 
        { 
            operation = "celsius_to_kelvin",
            input = temp,
            result = Math.Round(result, 2),
            formula = "K = °C + 273.15"
        });
    }

    public static IResult Compare(double temp1, string unit1, double temp2, string unit2)
    {
        var validUnits = new[] { "celsius", "fahrenheit", "kelvin", "c", "f", "k" };
        
        if (!validUnits.Contains(unit1.ToLower()) || !validUnits.Contains(unit2.ToLower()))
        {
            return Results.BadRequest(new 
            { 
                error = "Invalid temperature unit. Use: celsius/c, fahrenheit/f, or kelvin/k"
            });
        }

        try
        {
            // Convert both temperatures to Celsius for comparison
            var temp1Celsius = ConvertToCelsius(temp1, unit1.ToLower());
            var temp2Celsius = ConvertToCelsius(temp2, unit2.ToLower());

            var difference = Math.Round(Math.Abs(temp1Celsius - temp2Celsius), 2);
            string comparison;

            if (temp1Celsius > temp2Celsius)
                comparison = $"{temp1}° {NormalizeUnit(unit1)} is warmer than {temp2}° {NormalizeUnit(unit2)}";
            else if (temp1Celsius < temp2Celsius)
                comparison = $"{temp1}° {NormalizeUnit(unit1)} is cooler than {temp2}° {NormalizeUnit(unit2)}";
            else
                comparison = $"{temp1}° {NormalizeUnit(unit1)} is equal to {temp2}° {NormalizeUnit(unit2)}";

            return Results.Ok(new 
            { 
                operation = "compare_temperatures",
                comparison = comparison,
                difference = difference,
                unit = "Celsius degrees"
            });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    // Helper methods
    private static double ConvertToCelsius(double temp, string unit)
    {
        return unit switch
        {
            "celsius" or "c" => temp,
            "fahrenheit" or "f" => (temp - 32) * 5.0 / 9.0,
            "kelvin" or "k" => temp < 0 ? throw new ArgumentException("Temperature cannot be below absolute zero") : temp - 273.15,
            _ => throw new ArgumentException("Invalid temperature unit")
        };
    }

    private static string NormalizeUnit(string unit)
    {
        return unit.ToLower() switch
        {
            "celsius" or "c" => "Celsius",
            "fahrenheit" or "f" => "Fahrenheit",
            "kelvin" or "k" => "Kelvin",
            _ => unit
        };
    }
}