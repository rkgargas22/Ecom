using System.Text.Json.Serialization;

namespace Tmf.Ecom.Infrastructure.Models;

public class EcomPullShipmentTrackModel
{
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("pasword")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("awb")]
    public string Awb { get; set; } = string.Empty;
    [JsonPropertyName("order")]
    public string Order { get; set; } = string.Empty;
}
