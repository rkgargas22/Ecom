using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Tmf.Ecom.Infrastructure.HttpServices;

public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<Stream> GetAsync(string uri)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage response = await httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }

        string ss = await response.Content.ReadAsStringAsync();
        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<JsonDocument> PostAsync(string uri, HttpContent content)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        HttpResponseMessage response = await httpClient.PostAsync(uri, content);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        return await response.Content.ReadFromJsonAsync<JsonDocument>() ?? throw new ArgumentNullException();
    }
}
