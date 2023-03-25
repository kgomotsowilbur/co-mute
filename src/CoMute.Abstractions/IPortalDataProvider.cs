using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoMute.Abstractions.Models;

namespace CoMute.Abstractions;

public interface IPortalDataProvider
{
    void Initialize();
    Task<T> Get<T>(params object[] keyValues) where T : class;

    Task Insert<T>(T entity);
    Task Update<T>(T entity);

    #region [ User ]
    Task<Users> GetUser(string userId);
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users> GetUserDetails(string userId);
    #endregion

    #region [ CarPoolOpportunities ]
    Task<IEnumerable<CarPoolOpportunity>> GetAllCarPoolOpportunities();
    Task<CarPoolOpportunity> GetCarPoolOpportunity(Guid carPoolOpportunityId);
    #endregion
}

