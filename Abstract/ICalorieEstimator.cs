using CalorieApi.Models;

namespace CalorieApi.Abstract;

public interface ICalorieEstimator
{
    Task<FoodAnalysisResult> AnalyzeFoodAsync(byte[] imageBytes, string? userNotes);
}