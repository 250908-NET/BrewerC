public class Challenge7
{
    public static IResult Simple(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return Results.Ok(new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray()));
    }

    public static IResult Complex(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:,.<>?";
        var random = new Random();
        return Results.Ok(new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray()));
    }

    public static IResult Memorable(int words)
    {
        string[] wordList = new[] { "apple", "banana", "cat", "dog", "elephant", "fish", "grape", "hat", "ice", "jungle", "kite", "lemon", "monkey", "nut", "orange", "pear", "queen", "rose", "sun", "tree", "umbrella", "violet", "wolf", "xray", "yak", "zebra" };
        var random = new Random();
        var selectedWords = Enumerable.Range(0, words)
            .Select(_ => wordList[random.Next(wordList.Length)]);
        return Results.Ok(string.Join("-", selectedWords));
    }

    public static IResult Strength(string password)
    {
        int score = 0;
        if (password.Length >= 8) score++;
        if (password.Any(char.IsLower)) score++;
        if (password.Any(char.IsUpper)) score++;
        if (password.Any(char.IsDigit)) score++;
        if (password.Any(ch => "!@#$%^&*()-_=+[]{}|;:,.<>?".Contains(ch))) score++;

        return Results.Ok(score switch
        {
            5 => "Very Strong",
            4 => "Strong",
            3 => "Medium",
            2 => "Weak",
            _ => "Very Weak"
        });
    }
}