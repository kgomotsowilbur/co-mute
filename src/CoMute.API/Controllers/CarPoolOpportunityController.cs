using System;
using CoMute.Abstractions;
using CoMute.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoMute.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CarPoolOpportunityController : PortalControllerBase
{
    public CarPoolOpportunityController(IPortalDataProvider dataProvider) : base(dataProvider)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<CarPoolOpportunity>> GetAllCarPoolOpportunities()
    {
        try
        {
            var result = await this._dataprovider.GetAllCarPoolOpportunities();

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpGet("{carPoolOpportunityId}")]
    public async Task<CarPoolOpportunity> GetCarPoolOpportunity([FromRoute] Guid carPoolOpportunityId)
    {
        try
        {
            var result = await this._dataprovider.GetCarPoolOpportunity(carPoolOpportunityId);
            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    public async Task AddCarPoolOpportunity([FromBody] CarPoolOpportunity carPoolOpportunity)
    {
        try
        {
            await this._dataprovider.Insert(carPoolOpportunity);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

