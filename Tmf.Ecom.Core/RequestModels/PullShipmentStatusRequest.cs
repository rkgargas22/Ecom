using System.Text.Json.Serialization;

namespace Tmf.Ecom.Core.RequestModels;

public class PullShipmentStatusRequest
{
    [JsonPropertyName("awb")]
    public string Awb { get; set; } = string.Empty;
    [JsonPropertyName("order")]
    public string Order { get; set; } = string.Empty;
}
