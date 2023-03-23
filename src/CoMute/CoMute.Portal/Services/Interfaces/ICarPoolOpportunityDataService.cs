using System;
using AeverPortal.Abstractions.Models;

namespace AeverPortal.Portal.Services;

public interface ICarPoolOpportunityDataService
{
    Task AddFund(CarPoolOpportunity fund);
    Task<CarPoolOpportunity> GetFundDetails(Guid fundId);
    Task<CarPoolOpportunity> GetFund(Guid fundId);
    Task<IEnumerable<CarPoolOpportunity>> GetAllFunds();
    Task<IEnumerable<CarPoolOpportunity>> QueryFunds(CarPoolOpportunity query);
}
