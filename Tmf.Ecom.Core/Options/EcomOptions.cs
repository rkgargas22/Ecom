namespace Tmf.Ecom.Core.Options;

public class EcomOptions
{
    public const string Ecom = "Ecom";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public Url Url { get; set; }
}

public class Url
{
    public string GenerateManifest { get; set; } = string.Empty;
    public string RescheduleOrCancelRequest { get; set; } = string.Empty;
    public string GetShipmentDetails { get; set; } = string.Empty;
}
