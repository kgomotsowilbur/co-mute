using System;
using CoMute.Abstractions.Models;

namespace CoMute.Portal.Services;

public interface ICarPoolOpportunityDataService
{
    Task AddCarPoolOpportunity(CarPoolOpportunity carPoolOpportunity);
    Task<CarPoolOpportunity> GetCarPoolOpportunity(Guid carPoolOpportunityId);
    Task<IEnumerable<CarPoolOpportunity>> GetAllCarPoolOpportunities();
}
