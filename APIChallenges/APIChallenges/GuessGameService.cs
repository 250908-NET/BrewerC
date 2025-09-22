
public class GuessGameResponse
{
    public int Number { get; set; }
    public string Name { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int GameNumber { get; set; }
    public string Output { get; set; }

    public GuessGameResponse(
        int number, string name, int minValue, int maxValue, int gameNumber
        )
    {
        this.Number = number;
        this.Name = name;
        this.MinValue = minValue;
        this.MaxValue = maxValue;
        this.GameNumber = gameNumber;
        this.Output = "";
    }
}

public class GameInfo
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int GameNumber { get; set; }
}

public class GuessGameService
{
    private readonly Random _random = new Random();
    private int _gameNumber = -1;
    private int _randomNumber = -1;
    private List<string> events = new List<string>();
    public int GameNumber => _gameNumber;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public GuessGameService(int minValue, int maxValue)
    {
        this.MinValue = minValue;
        this.MaxValue = maxValue;
        this.ResetGame();
    }

    public GuessGameResponse GuessNumber(int number, string name)
    {
        this.events.Add($"{name} guessed the number {number}");
        // string newEvent = $"{name} guessed the number {number}";
        GuessGameResponse response = new GuessGameResponse(
            number, name, this.MinValue, this.MaxValue, this.GameNumber
        );

        if (number == this._randomNumber)
        {
            response.Output = "Correct! You guessed the number.";
            // newEvent += "Their guess was CORRECT!";
            this.events.Add($"{name} CORRECTLY GUESSED {this._randomNumber}!");
            this.ResetGame();
        }
        else if (number < this._randomNumber)
        {
            response.Output = "Too low. Try again.";
            // newEvent += "Their guess was too low.";
            // this.events.Add($"{name} correctly guessed {this._randomNumber}");
        }
        else
        {
            response.Output = "Too high. Try again.";
            // newEvent += "Their guess was too high.";
        }

        return response;
    }

    public List<string> GetEvents()
    {
        return this.events;
    }

    public GameInfo GetGameInfo()
    {
        return new GameInfo { Min = this.MinValue, Max = this.MaxValue, GameNumber = this.GameNumber };
    }

    private void ResetGame()
    {
        this._randomNumber = this._random.Next(this.MinValue, this.MaxValue);
        this._gameNumber++;
        this.events.Add($"Creating game #{this.GameNumber}");
    }
}
