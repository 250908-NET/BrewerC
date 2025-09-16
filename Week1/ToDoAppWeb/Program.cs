using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Content("<a href='/playerCount'>Get Player Count </a>", "text/html"));

app.MapGet("/health", () => new {
    status = "Healthy",
    message = "Hello World!"
});


int getPlayerTotal() {
    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("request");
    string url = "https://usnode1.spearheadservers.com:8443/health";

    var result = httpClient.GetAsync(url).Result;
    if (result.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) return -1;

    var json = result.Content.ReadAsStringAsync().Result;
    using var doc = JsonDocument.Parse(json);
    if (!doc.RootElement.TryGetProperty("player_count", out var playerCountElement)) return -1;

    return playerCountElement.GetInt32();
}

app.MapGet("/playerCount", () => {
    int playerCount = getPlayerTotal();
    if (playerCount == -1) {
        return Results.Problem("Could not retrieve player count", statusCode: 503);
    }
    return Results.Ok(new { player_count = playerCount });
});


app.Run();
