using System;
using System.Text;
using CoMute.Abstractions.Models;
using Newtonsoft.Json;

namespace CoMute.Portal.Services;
public class UserDataService : IUserDataService
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
    protected string ApiPath { get; } = "/user";
    #endregion

    public Task<IEnumerable<Users>> GetAllUsers()
    {
        return this.GetJsonResults<IEnumerable<Users>>(this.ApiPath);
    }

    public Task<Users> GetUser(Guid userId)
    {
        return this.GetJsonResults<Users>($"{this.ApiPath}/{userId}");
    }

    public Task<Users> GetUserDetails(Guid userId)
    {
        return this.GetJsonResults<Users>($"{this.ApiPath}/{userId}/userDetails");
    }

    public async Task AddUser(Users user)
    {
        string userJson = JsonConvert.SerializeObject(user, Formatting.Indented);

        var content = new StringContent(userJson, Encoding.UTF8, JSON_MEDIA_TYPE);

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
