using System;
using AeverPortal.Abstractions.Models;

namespace AeverPortal.Portal.Services;

public interface IFundManagerDataService
{
    Task<User> GetFundManager(Guid fundManagerId);
    Task<IEnumerable<User>> GetAllFundManager();
    Task AddFundManager(User fundManager);
    Task<User> GetFundManagerDetails(Guid fundManagerId);
}

