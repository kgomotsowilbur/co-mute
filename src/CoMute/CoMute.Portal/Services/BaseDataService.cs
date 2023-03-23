using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AeverPortal.Portal.Services;

public abstract class BaseDataService
{
    #region [ CTor ]
    public BaseDataService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }
    #endregion

    #region [ Constants ]
    public const string JSON_MEDIA_TYPE = "application/json";
    #endregion

    #region [ Fields ]
    protected readonly HttpClient _httpClient;
    #endregion

    #region [ Properties ]
    protected virtual string ApiPath { get; }
    #endregion

    protected StringContent PrepareJsonPayload(object payloadContentObject)
    {
        var serializedPayload = JsonConvert.SerializeObject(payloadContentObject);
        var result = new StringContent(serializedPayload, System.Text.Encoding.UTF8, JSON_MEDIA_TYPE);

        return result;
    }

    protected async Task<string> GetStringResult(string requestUrl)
    {
        var response = await _httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    protected async Task<T> GetJsonResults<T>(string requestUrl)
    {
        var response = await _httpClient.GetStringAsync(requestUrl);

        var result = JsonConvert.DeserializeObject<T>(response);

        return result;
    }

    protected async Task<T> SendAsync<T>(string requestUrl, HttpMethod httpMethod)
    {
        var reqMsq = new HttpRequestMessage(httpMethod, requestUrl);
        var response = await _httpClient.SendAsync(reqMsq);
        //response.EnsureSuccessStatusCode();

        var respStream = await response.Content.ReadAsStringAsync();
        T result = JsonConvert.DeserializeObject<T>(respStream);

        return result;
    }
}