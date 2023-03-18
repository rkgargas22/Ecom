namespace Tmf.Ecom.Infrastructure.HttpServices;

public interface IHttpService
{
    Task<Stream> GetAsync(string uri);
    Task<JsonDocument> PostAsync(string uri, HttpContent content);
}
