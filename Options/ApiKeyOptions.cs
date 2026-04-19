using System.ComponentModel.DataAnnotations;

namespace CalorieApi.Options;

public class ApiKeyOptions
{
    public const string SectionName = "ApiKeys";

    [Required(ErrorMessage = "Google API Key is missing in configuration (ApiKeys:Google)")]
    public string Google { get; set; } = string.Empty;
}
