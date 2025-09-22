public class StringService
{
    private readonly ILogger<StringService> _logger;

    public StringService(ILogger<StringService> logger)
    {
        _logger = logger;
    }

    public IResult ReverseText(string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return Results.BadRequest("Text cannot be empty");
            }

            var reversed = new string(text.Reverse().ToArray());
            
            _logger.LogInformation("Text reversed: '{Original}' -> '{Reversed}'", text, reversed);

            return Results.Ok(new TextResponse
            {
                Original = text,
                Result = reversed,
                Operation = "reverse"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reversing text");
            return Results.BadRequest("Error processing text");
        }
    }

    public IResult UppercaseText(string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return Results.BadRequest("Text cannot be empty");
            }

            var uppercase = text.ToUpper();
            
            _logger.LogInformation("Text uppercased: '{Original}' -> '{Result}'", text, uppercase);

            return Results.Ok(new TextResponse
            {
                Original = text,
                Result = uppercase,
                Operation = "uppercase"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting text to uppercase");
            return Results.BadRequest("Error processing text");
        }
    }

    public IResult LowercaseText(string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return Results.BadRequest("Text cannot be empty");
            }

            var lowercase = text.ToLower();
            
            _logger.LogInformation("Text lowercased: '{Original}' -> '{Result}'", text, lowercase);

            return Results.Ok(new TextResponse
            {
                Original = text,
                Result = lowercase,
                Operation = "lowercase"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting text to lowercase");
            return Results.BadRequest("Error processing text");
        }
    }

    public IResult CountText(string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return Results.BadRequest("Text cannot be empty");
            }

            var characterCount = text.Length;
            var wordCount = text.Split(new char[] { ' ', '\t', '\n', '\r' }, 
                StringSplitOptions.RemoveEmptyEntries).Length;
            
            var vowels = "aeiouAEIOU";
            var vowelCount = text.Count(c => vowels.Contains(c));

            var response = new TextCountResponse
            {
                Original = text,
                CharacterCount = characterCount,
                WordCount = wordCount,
                VowelCount = vowelCount
            };

            _logger.LogInformation("Text counted: '{Text}' - {Characters} chars, {Words} words, {Vowels} vowels", 
                text, characterCount, wordCount, vowelCount);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting text");
            return Results.BadRequest("Error processing text");
        }
    }

    public IResult CheckPalindrome(string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return Results.BadRequest("Text cannot be empty");
            }

            // Clean text for palindrome check (remove spaces, punctuation, make lowercase)
            var cleanText = new string(text.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            
            var reversed = new string(cleanText.Reverse().ToArray());
            var isPalindrome = cleanText == reversed;

            var response = new PalindromeResponse
            {
                Original = text,
                CleanedText = cleanText,
                IsPalindrome = isPalindrome,
                Message = isPalindrome 
                    ? "Yes, this is a palindrome!" 
                    : "No, this is not a palindrome."
            };

            _logger.LogInformation("Palindrome check: '{Original}' -> '{Cleaned}' -> {Result}", 
                text, cleanText, isPalindrome ? "IS palindrome" : "NOT palindrome");

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking palindrome");
            return Results.BadRequest("Error processing text");
        }
    }
}

// Response Models
public record TextResponse
{
    public string Original { get; init; } = "";
    public string Result { get; init; } = "";
    public string Operation { get; init; } = "";
}

public record TextCountResponse
{
    public string Original { get; init; } = "";
    public int CharacterCount { get; init; }
    public int WordCount { get; init; }
    public int VowelCount { get; init; }
}

public record PalindromeResponse
{
    public string Original { get; init; } = "";
    public string CleanedText { get; init; } = "";
    public bool IsPalindrome { get; init; }
    public string Message { get; init; } = "";
}