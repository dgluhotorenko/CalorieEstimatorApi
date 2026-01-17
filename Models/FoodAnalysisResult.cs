using System.Text.Json.Serialization;

namespace CalorieApi.Models;

public record FoodAnalysisResult
{
    [JsonPropertyName("is_food")]
    public bool IsFood { get; set; } = true;

    [JsonPropertyName("dish_name")]
    public string DishName { get; set; } = "Unknown";

    [JsonPropertyName("total_calories")]
    public int TotalCalories { get; set; }

    [JsonPropertyName("ingredients")]
    public List<Ingredient> Ingredients { get; set; } = new();

    [JsonPropertyName("confidence_score")]
    public int ConfidenceScore { get; set; }

    [JsonIgnore]
    public int UsageTotalTokens { get; set; }
}