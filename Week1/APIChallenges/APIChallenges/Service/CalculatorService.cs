using Microsoft.AspNetCore.Http;
public class CalculatorService
{
     public CalculatorResult Add(double a, double b) => new CalculatorResult("add", a, b, a + b);

    public CalculatorResult Subtract(double a, double b) => new CalculatorResult("subtract", a, b, a - b);

    public CalculatorResult Multiply(double a, double b) => new CalculatorResult("multiply", a, b, a * b);

    public IResult Divide(double a, double b)
    {
        if (b == 0)
            return Results.BadRequest(new { error = "Cannot divide by zero" });

        return Results.Ok(new CalculatorResult("divide", a, b, a / b));
    }
}