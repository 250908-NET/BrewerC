
public class Challenge3
{
    public static IResult fizzbuzz(int count)
    {
        if (count < 1) return Results.Content("Input must be greater than 0", "text/html", statusCode: 400);
        string output = "";
        for (int i = 1; i <= count; i++)
        {
            if (i % 3 == 0) output += "Fizz";
            if (i % 5 == 0) output += "Buzz";
            if (i % 3 == 0 || i % 5 == 0) output += "<br>";
        }
        return Results.Content(output, "text/html", statusCode: 200);
    }

    public static IResult isPrime(int number)
    {
        bool isPrime = false;
        if (number <= 1) isPrime = false;
        else if (number <= 3) isPrime = true;
        else if (number % 2 == 0 || number % 3 == 0) isPrime = false;
        else
        {
            bool checkIsPrime = true;
            for (int i = 5; i * i <= number; i+=6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            isPrime = checkIsPrime;
        }
        return Results.Content($"{{\"isPrime\": {isPrime} }}", "application/json", statusCode: 200);
    }
}
