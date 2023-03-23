using System;
using AeverPortal.Abstractions;
using AeverPortal.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace AeverPortal.FrontEndAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FundController : PortalControllerBase
{
    public FundController(IPortalDataProvider dataProvider) : base(dataProvider)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<CarPoolOpportunity>> GetAllFunds()
    {
        try
        {
            var result = await this._dataprovider.GetAllFunds();

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpGet("{fundId}")]
    public async Task<CarPoolOpportunity> GetAsync([FromRoute] Guid fundId)
    {
        try
        {
            var result = await this._dataprovider.GetFund(fundId);
            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    public async Task AddFund([FromBody] CarPoolOpportunity fund)
    {
        try
        {
            await this._dataprovider.Insert(fund);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [Route("addInvestmentTeamMember")]
    [HttpPost]
    public async Task AddInvestmentTeamMemberToFund([FromBody] InvestmentTeamMember investmentTeamMember)
    {
        try
        {
            await this._dataprovider.Insert(investmentTeamMember);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("{fundId}/fundDetails")]
    public async Task<CarPoolOpportunity> GetFundDetails([FromRoute] Guid fundId)
    {
        try
        {
            var result = await this._dataprovider.GetFundDetails(fundId);
            return result;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [Route("queryFunds")]
    [HttpPost]
    public async Task<IEnumerable<CarPoolOpportunity>> QueryFunds([FromBody] CarPoolOpportunity query)
    {
        try
        {
            if(query.Name == null) {
                query.Name = "_._";
            }
            if(query.ValueCreation == null) {
                query.ValueCreation = "_._";
            } 
            if(query.Type == null) {
                query.Type = "_._";
            }
            if(query.Status == null) { 
                query.Status = "_._";
            }
            if(query.InvestmentStrategy == null) { 
                query.InvestmentStrategy = "_._";
            }

            var result = await this._dataprovider.QueryFunds(query);
            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}

