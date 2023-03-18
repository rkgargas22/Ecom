using System.Text.Json.Serialization;

namespace Tmf.Ecom.Core.ResponseModels;

public class PushShipmentTrackResponse
{
    [JsonPropertyName("status")] 
    public bool Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
