using System;
using System.Text;
using AeverPortal.Abstractions.Models;
using Newtonsoft.Json;

namespace AeverPortal.Portal.Services;

public class CarPoolOpportunityDataService : ICarPoolOpportunityDataService
{
    #region [ CTor ]
    public CarPoolOpportunityDataService(HttpClient httpClient)
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
    protected string ApiPath { get; } = "/fund";
    #endregion

    public Task<IEnumerable<CarPoolOpportunity>> GetAllFunds()
    {
        return this.GetJsonResults<IEnumerable<CarPoolOpportunity>>(this.ApiPath);
    }

    public Task<CarPoolOpportunity> GetFund(Guid fundId )
    {
        return this.GetJsonResults<CarPoolOpportunity>($"{this.ApiPath}/{fundId}");
    }
    
    public Task<CarPoolOpportunity> GetFundDetails(Guid fundId)
    {
        return this.GetJsonResults<CarPoolOpportunity>($"{this.ApiPath}/{fundId}/fundDetails");
    }

    public async Task AddFund(CarPoolOpportunity fund)
    {
        string userJson = JsonConvert.SerializeObject(fund, Formatting.Indented);

        var content = new StringContent(userJson, Encoding.UTF8, JSON_MEDIA_TYPE);

        var result = await _httpClient.PostAsync(this.ApiPath, content);
        result.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<CarPoolOpportunity>> QueryFunds(CarPoolOpportunity query)
    {
        string userJson = JsonConvert.SerializeObject(query, Formatting.Indented);

        var content = new StringContent(userJson, Encoding.UTF8, JSON_MEDIA_TYPE);

        var result = await _httpClient.PostAsync($"{this.ApiPath}/queryFunds", content);

        var responseJson = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<CarPoolOpportunity>>(responseJson);
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

