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
            randomNumber = random.Next(1, 21);
            gameNumber += 1;
        }
        string jsonOutput = $"\"player\": \"{name}\", \"number\": {number}";
        jsonOutput += $", \"gameNumber\": {gameNumber}";
        jsonOutput += $", \"isCorrect\": {(number == randomNumber ? "true" : "false")}";
        jsonOutput += ", \"minValue\": 1, \"maxValue\": 20";
        jsonOutput += $", \"previousGame\": {lastGameJson}";
        if (number == randomNumber)
        {
            jsonOutput += ", \"output\": \"You win!\"";
            lastGameJson = $"{{ \"previousGameNumber\": {gameNumber}, \"previousGameOutput\": \"{name} correctly guessed {randomNumber}\" }}";
            gameNumber++;
            randomNumber = random.Next(1, 21);
        }
        else
        {
            jsonOutput += ", \"output\": \"Your guess is incorrect.\"";
        }
        return Results.Content($"{{ {jsonOutput} }}", "application/json", statusCode: StatusCodes.Status200OK);
    }
    public static IResult RockPaperScissors(string choice)
    {
        string[] options = { "rock", "paper", "scissors" };
        string userChoice = choice.ToLower();
        if (!options.Contains(userChoice))
            return Results.BadRequest("Invalid choice. Use rock, paper, or scissors.");
        string aiChoice = options[random.Next(options.Length)];
        string result;
        if (userChoice == aiChoice)
            result = "Draw";
        else if (
            (userChoice == "rock" && aiChoice == "scissors") ||
            (userChoice == "paper" && aiChoice == "rock") ||
            (userChoice == "scissors" && aiChoice == "paper")
        )
            result = "You win!";
        else
            result = "You lose!";
            
        return Results.Json(new { user = userChoice, ai = aiChoice, result });
    }
    public static IResult RollDice(int sides, int count)
    {
        if (sides < 2 || count < 1 || count > 100)
            return Results.BadRequest("Invalid dice parameters.");
        var rolls = new List<int>();
        for (int i = 0; i < count; i++)
            rolls.Add(random.Next(1, sides + 1));
        return Results.Json(new { sides, count, rolls });
    }
    public static IResult CoinFlip(int count)
    {
        if (count < 1 || count > 100)
            return Results.BadRequest("Invalid count.");
        var results = new List<string>();
        for (int i = 0; i < count; i++)
            results.Add(random.Next(2) == 0 ? "Heads" : "Tails");
        return Results.Json(new { count, results });
    }
}