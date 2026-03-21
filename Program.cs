using CalorieApi.Abstract;
using CalorieApi.Middleware;
using CalorieApi.Options;
using CalorieApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calorie Analysis API", Version = "v1" });
});

builder.Services.AddOptions<ApiKeyOptions>()
    .BindConfiguration(ApiKeyOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<ICalorieEstimator, GoogleGeminiEstimator>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calorie API V1");
        c.RoutePrefix = "swagger"; 
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseExceptionHandler();

app.MapGet("/", () => "Calorie API is running. Go to /swagger for UI.").ExcludeFromDescription();

app.MapPost("/api/analyze", async (IFormFile image,
        [FromForm] string? notes,
        [FromServices] ICalorieEstimator estimator) =>
    {
        if (image.Length == 0)
            return Results.BadRequest("Image is required");

        if (image.Length > 5 * 1024 * 1024)
            return Results.BadRequest("Image is too large (max 5MB)");

        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();

        var result = await estimator.AnalyzeFoodAsync(imageBytes, notes);
        return Results.Ok(result);
    })
    .WithName("AnalyzeFood")
    .DisableAntiforgery();

app.Run();

public partial class Program;
