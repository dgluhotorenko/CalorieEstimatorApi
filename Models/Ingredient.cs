using System.Text.Json.Serialization;

namespace CalorieApi.Models;

public record Ingredient
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("weight_grams")]
    public int WeightGrams { get; set; }

    [JsonPropertyName("calories")]
    public int Calories { get; set; }

    [JsonPropertyName("protein")]
    public double Protein { get; set; }

    [JsonPropertyName("fat")]
    public double Fat { get; set; }

    [JsonPropertyName("carbs")]
    public double Carbs { get; set; }
}