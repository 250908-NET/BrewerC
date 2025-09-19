public class Challenge11
{

    public static Random random = new Random();
    public static int gameNumber = -1;
    public static int randomNumber = -1;
    public static string lastGameJson = "{ \"previousGameNumber\": -1, \"previousGameOutput\": \"There was no previous game\" }";
    public static IResult GuessNumber(int number, string name)
    {
        if (randomNumber == -1)
        {
            Challenge11.randomNumber = Challenge11.random.Next(1, 20);
            Challenge11.gameNumber += 1;
        }
        string jsonOutput = $"\"player\": \"{name}\", \"number\": {number}";
        jsonOutput += $", \"gameNumber\": {Challenge11.gameNumber}";
        // jsonOutput += $", \"isCorrect\": {number == Challenge11.randomNumber ? "true" : "false"}";
        jsonOutput += $", \"isCorrect\": {(number == Challenge11.randomNumber ? "true" : "false")}";
        jsonOutput += ", \"minValue\": 1, \"maxValue\": 20";
        jsonOutput += $", \"previousGame\": {Challenge11.lastGameJson}";

        if (number == Challenge11.randomNumber)
        {
            jsonOutput += ", \"output\": \"You win!\"";
            Challenge11.lastGameJson = $"{{ \"previousGameNumber\": {Challenge11.gameNumber}, \"previousGameOutput\": \"{name} correctly guessed {Challenge11.randomNumber}\" }}";
            Challenge11.gameNumber++;
            Challenge11.randomNumber = Challenge11.random.Next(1, 20);
        }
        else
        {
            jsonOutput += ", \"output\": \"Your guess is incorrect.\"";
        }

        return Results.Content($"{{ {jsonOutput} }}", "application/json", statusCode: StatusCodes.Status200OK);
    }
    public static IResult RockPaperScissors(string choice)
    {
        return Results.Ok("Hello");
    }
    public static IResult RollDice(int sides, int count)
    {
        return Results.Ok("Hello");
    }
    public static IResult CoinFlip(int count)
    {
        return Results.Ok("Hello");
    }
}