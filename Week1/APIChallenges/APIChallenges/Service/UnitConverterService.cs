public class UnitConverterService
{
    public double convertLength(double value, string fromUnit, string toUnit)
    {
        fromUnit = fromUnit.ToLower();
        toUnit = toUnit.ToLower();

        double valueInMeters;

        //Covert input to Meter
        switch (fromUnit)
        {
            case "meter":
                valueInMeters = value;
                break;
            case "feet":
                valueInMeters = value * 0.3048;
                break;
            case "inch":
                valueInMeters = value * 0.0254;
                break;
            default:
                throw new ArgumentException("Unsupported fromUnit");
        }

        switch (toUnit)
        {
            case "meter":
                return valueInMeters;
            case "feet":
                return valueInMeters / 0.3048;
            case "inch":
                return valueInMeters / 0.0254;
            default:
                throw new ArgumentException("UnsupportedToUnit");
        }
    }

    public double convertWeight(double value, string fromUnit, string toUnit)
    {
        fromUnit = fromUnit.ToLower();
        toUnit = toUnit.ToLower();

        double valueInKgs;

        //Covert input to Kgs
        switch (fromUnit)
        {
            case "kg":
                valueInKgs = value;
                break;
            case "lbs":
                valueInKgs = value * 0.453592;
                break;
            case "ounces":
                valueInKgs = value * 0.0283495;
                break;
            default:
                throw new ArgumentException("Unsupported fromUnit");
        }

        switch (toUnit)
        {
            case "kg":
                return valueInKgs;
            case "lbs":
                return valueInKgs / 453592;
            case "ounces":
                return valueInKgs / 0.0283495;
            default:
                throw new ArgumentException("Unsupported toUnit");
        }
    }

    public double convertVolume(double value, string fromUnit, string toUnit)
{
    fromUnit = fromUnit.ToLower();
    toUnit = toUnit.ToLower();

    double valueInLiters;

    // Convert input to Liters
    switch (fromUnit)
    {
        case "liter":
            valueInLiters = value;
            break;
        case "gallon":
            valueInLiters = value * 3.78541;
            break;
        case "cup":
            valueInLiters = value * 0.236588;
            break;
        default:
            throw new ArgumentException("Unsupported fromUnit");
    }

    // Convert Liters to target unit
    switch (toUnit)
    {
        case "liter":
            return valueInLiters;
        case "gallon":
            return valueInLiters / 3.78541;
        case "cup":
            return valueInLiters / 0.236588;
        default:
            throw new ArgumentException("Unsupported toUnit");
    }
}
}