using System;
using System.Text;
using CoMute.Abstractions.Models;
using Newtonsoft.Json;

namespace CoMute.Portal.Services;

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
    protected string ApiPath { get; } = "/carPoolOpportunity";
    #endregion

    public Task<IEnumerable<CarPoolOpportunity>> GetAllCarPoolOpportunities()
    {
        return this.GetJsonResults<IEnumerable<CarPoolOpportunity>>(this.ApiPath);
    }

    public Task<CarPoolOpportunity> GetCarPoolOpportunity(Guid carPoolOpportunityId )
    {
        return this.GetJsonResults<CarPoolOpportunity>($"{this.ApiPath}/{carPoolOpportunityId}");
    }

    public async Task AddCarPoolOpportunity(CarPoolOpportunity carPoolOpportunity)
    {
        string carPoolOpportunityJson = JsonConvert.SerializeObject(carPoolOpportunity, Formatting.Indented);

        var content = new StringContent(carPoolOpportunityJson, Encoding.UTF8, JSON_MEDIA_TYPE);

        var result = await _httpClient.PostAsync(this.ApiPath, content);
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

