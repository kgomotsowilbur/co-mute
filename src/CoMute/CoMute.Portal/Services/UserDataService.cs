using System;
using System.Text;
using AeverPortal.Abstractions.Models;
using Newtonsoft.Json;

namespace AeverPortal.Portal.Services;
public class UserDataService : IFundManagerDataService
{
    #region [ CTor ]
    public UserDataService(HttpClient httpClient)
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
    protected string ApiPath { get; } = "/fundManager";
    #endregion

    public Task<IEnumerable<User>> GetAllFundManager()
    {
        return this.GetJsonResults<IEnumerable<User>>(this.ApiPath);
    }

    public Task<User> GetFundManager(Guid fundManagerId)
    {
        return this.GetJsonResults<User>($"{this.ApiPath}/{fundManagerId}");
    }

    public Task<User> GetFundManagerDetails(Guid fundManagerId)
    {
        return this.GetJsonResults<User>($"{this.ApiPath}/{fundManagerId}/fundManagerDetails");
    }

    public async Task AddFundManager(User fundManager)
    {
        string fundManagerJson = JsonConvert.SerializeObject(fundManager, Formatting.Indented);

        var content = new StringContent(fundManagerJson, Encoding.UTF8, JSON_MEDIA_TYPE);

        var result = await _httpClient.PostAsync(ApiPath, content);
        result.EnsureSuccessStatusCode();
    }

    #region [ Private Methods ]
    protected async Task<T> GetJsonResults<T>(string requestUrl)
    {
        var response = await _httpClient.GetStringAsync(requestUrl);

        var result = JsonConvert.DeserializeObject<T>(response);

        return result;
    }
    #endregion
}
