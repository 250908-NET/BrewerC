public class ColorsService
{
    private readonly List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
    public List<string> getColors()
    {
        return colors;
    }

    public string getRandomColor()
    {
        Random rnd = new Random();
        int index = rnd.Next(colors.Count);
        return colors[index];
    }

    public List<string> searchColor(string letter)
    {
        return colors.Where(f => f.StartsWith(letter, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public bool addColor(string color)
{
    if (colors.Any(c => c.Equals(color, StringComparison.OrdinalIgnoreCase)))
    {
        return false; // already exists
    }

    colors.Add(color);
    return true; // added successfully
}
}  