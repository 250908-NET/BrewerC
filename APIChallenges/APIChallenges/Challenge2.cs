public class Challenge2
{
    public static IResult Reverse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Results.BadRequest(new { error = "Text cannot be empty" });

        var result = new string(text.Reverse().ToArray());
        return Results.Ok(new { operation = "reverse", original = text, result = result });
    }

    public static IResult Uppercase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Results.BadRequest(new { error = "Text cannot be empty" });

        var result = text.ToUpper();
        return Results.Ok(new { operation = "uppercase", original = text, result = result });
    }

    public static IResult Lowercase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Results.BadRequest(new { error = "Text cannot be empty" });

        var result = text.ToLower();
        return Results.Ok(new { operation = "lowercase", original = text, result = result });
    }

    public static IResult Count(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Results.BadRequest(new { error = "Text cannot be empty" });

        var characterCount = text.Length;
        var wordCount = CountWords(text);
        var vowelCount = CountVowels(text);

        return Results.Ok(new 
        { 
            operation = "count", 
            text = text,
            characterCount = characterCount,
            wordCount = wordCount,
            vowelCount = vowelCount
        });
    }

    public static IResult Palindrome(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Results.BadRequest(new { error = "Text cannot be empty" });

        var isPalindrome = IsPalindrome(text);
        return Results.Ok(new 
        { 
            operation = "palindrome", 
            text = text,
            isPalindrome = isPalindrome
        });
    }

    // Helper methods
    private static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        var words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, 
                              StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    private static int CountVowels(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        var vowels = "aeiouAEIOU";
        return text.Count(c => vowels.Contains(c));
    }

    private static bool IsPalindrome(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        // Remove spaces and punctuation, convert to lowercase
        var cleanText = new string(text.ToLower().Where(c => char.IsLetterOrDigit(c)).ToArray());
        
        // Compare with its reverse
        var reversed = new string(cleanText.Reverse().ToArray());
        return cleanText == reversed;
    }
}