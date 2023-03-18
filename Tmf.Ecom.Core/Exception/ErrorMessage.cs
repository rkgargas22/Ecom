using System.Text.Json.Serialization;

namespace Tmf.Ecom.Core.Exception;

public class ErrorMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("error")]
    public dynamic Error { get; set; } = string.Empty;
}
