**CalorieAI Web API**

A high-performance .NET Web API that leverages LLMs to estimate the nutritional composition of dishes from photographs. This serves as the backend engine for the CalorieAI mobile ecosystem.

**Core Capabilities**

Visual Food Recognition: Analyzes images to identify ingredients and portion sizes.

Nutritional Estimation: Calculates calories, proteins, fats, and carbohydrates.

Context-Aware: Supports user notes (e.g., "hidden sauce" or "extra protein") to refine AI accuracy.

OpenAPI Support: Fully documented with Swagger UI for easy testing.

**Getting Started**

Prerequisites

.NET 8.0 SDK (or later)

Google Gemini API Key (Get it at [Google AI Studio]([url](https://aistudio.google.com/)))

**Installation & Run**

Clone the repository:

git clone [https://github.com/dgluhotorenko/CalorieEstimatorApi.git](https://github.com/dgluhotorenko/CalorieEstimatorApi.git)


**Configure API Key:**
Open appsettings.json and insert your Google API Key:

{
  "ApiKeys": {
    "Google": "YOUR_API_KEY_HERE"
  }
}


**Launch the API:**
Press F5 in Rider/Visual Studio or run via CLI:

dotnet run


**Access Swagger UI:**
Navigate to http://localhost:YOUR_PORT/swagger to explore and test the endpoints.

Remote Testing (Mobile Integration)

To test the API from a physical mobile device or external network without deployment, use Microsoft Dev Tunnels:

**Install DevTunnel CLI:**

winget install Microsoft.DevTunnels


Login:

devtunnel user login


Host the Tunnel:
With your API running, execute:

devtunnel host -p YOUR_PORT --allow-anonymous


Note: Replace YOUR_PORT with your local port (e.g., 5064).

**Connect:**
Use the generated HTTPS URL (e.g., https://random-id.devtunnels.ms) in your Mobile Client configuration.

**Technical Details**

Technology: ASP.NET Core Web API

AI Integration: Google Gemini 1.5 Flash (via REST API)

Format: Returns structured JSON mapped to FoodAnalysisResult model.

Developed as the backend for the CalorieAI project.
