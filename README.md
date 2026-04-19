# CalorieAI Web API

A .NET Web API that leverages Google Gemini to estimate the nutritional composition of dishes from photographs. Backend for the [CalorieAI Mobile Client](https://github.com/dgluhotorenko/CalorieEstimatorClient).

## Features

- **Visual Food Recognition** — analyzes images to identify ingredients and portion sizes
- **Nutritional Estimation** — calculates calories, proteins, fats, and carbohydrates per ingredient
- **Context-Aware** — supports user notes (e.g., "hidden sauce", "extra cheese") to refine accuracy
- **OpenAPI Support** — fully documented with Swagger UI

## Tech Stack

| | |
|---|---|
| Framework | ASP.NET Core Minimal API (.NET 10) |
| AI Model | Google Gemini 2.0 Flash |
| Error Handling | Global `IExceptionHandler` + ProblemDetails (RFC 9457) |
| Configuration | Options pattern with startup validation |
| Testing | xUnit + NSubstitute + WebApplicationFactory |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Google Gemini API Key](https://aistudio.google.com/)

### Setup

```bash
git clone https://github.com/dgluhotorenko/CalorieEstimatorApi.git
cd CalorieEstimatorApi
```

Configure your API key using [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) (keys stay local, never in git):

```bash
dotnet user-secrets set "ApiKeys:Google" "YOUR_API_KEY"
```

Run with the `http` launch profile (the mobile client expects the API on `http://localhost:5064`):

```bash
dotnet run --launch-profile http
```

Open Swagger UI at `http://localhost:5064/swagger`.

### Testing from a Mobile Device

To expose the API to a physical device without deployment, use [Dev Tunnels](https://learn.microsoft.com/en-us/azure/developer/dev-tunnels/):

```bash
winget install Microsoft.DevTunnels
devtunnel user login
devtunnel host -p 5064 --allow-anonymous
```

Use the generated HTTPS URL in your mobile client configuration.

## API

### `POST /api/analyze`

Accepts `multipart/form-data`:

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `image` | file | yes | Photo of the dish (max 5 MB) |
| `notes` | string | no | Additional context for the AI |

Returns a JSON `FoodAnalysisResult` with dish name, total calories, confidence score, and per-ingredient breakdown (weight, calories, protein, fat, carbs).

## Running Tests

```bash
cd CalorieApi.Tests
dotnet test
```

## The call via Swagger
<img width="983" height="983" alt="image" src="https://raw.githubusercontent.com/dgluhotorenko/CalorieApi/master/docs/swagger.png" />
