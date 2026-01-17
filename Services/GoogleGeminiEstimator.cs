using System.Text.Json;
using CalorieApi.Abstract;
using CalorieApi.Models;

namespace CalorieApi.Services;

public class GoogleGeminiEstimator(string apiKey, IHttpClientFactory httpClientFactory) : ICalorieEstimator
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private const string Model = "gemini-2.0-flash";

    public async Task<FoodAnalysisResult> AnalyzeFoodAsync(byte[] imageBytes, string? userNotes)
    {
        var base64Image = Convert.ToBase64String(imageBytes);
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={apiKey}";

        var promptText = PromptConstants.SystemPrompt;
        if (!string.IsNullOrWhiteSpace(userNotes))
        {
            promptText += $"\n\n[USER CONTEXT]: The user provided the following notes about the food (e.g. hidden ingredients, preparation method): \"{userNotes}\". Use this information to improve accuracy.";
        }

        var payload = new
        {
            contents = new[]
            {
                new
                {
                    parts = new object[]
                    {
                        new { text = promptText },
                        new
                        {
                            inline_data = new
                            {
                                mime_type = "image/jpeg",
                                data = base64Image
                            }
                        }
                    }
                }
            },
            generationConfig = new
            {
                response_mime_type = "application/json"
            }
        };

        var response = await _httpClient.PostAsJsonAsync(url, payload);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Google API Error: {error}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);

        var textResult = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        textResult = textResult?.Replace("```json", "").Replace("```", "").Trim();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<FoodAnalysisResult>(textResult, options) ?? throw new Exception("Failed to deserialize JSON");

        if (doc.RootElement.TryGetProperty("usageMetadata", out var usage))
        {
            if (usage.TryGetProperty("totalTokenCount", out var totalTokens))
            {
                result.UsageTotalTokens = totalTokens.GetInt32();
            }
        }

        return result;
    }
}