using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AeverPortal.Abstractions.Models;

namespace AeverPortal.Abstractions;

public interface IPortalDataProvider
{
    void Initialize();
    Task<T> Get<T>(params object[] keyValues) where T : class;

    Task Insert<T>(T entity);
    Task Update<T>(T entity);

    #region [ TeamMember ]
    Task<TeamMember> GetTeamMember(Guid userId);
    Task<IEnumerable<TeamMember>> GetAllTeamMembers();
  //  Task<TeamMember> GetTeamMemberDetails(Guid userId);
    #endregion

    #region [ FundManager ]
    Task<User> GetFundManager(Guid userId);
    Task<IEnumerable<User>> GetAllFundManagers();
    Task<User> GetFundManagerDetails(Guid userId);
    #endregion

    #region [ Funds ]
    Task<IEnumerable<CarPoolOpportunity>> GetAllFunds();
    Task<IEnumerable<CarPoolOpportunity>> QueryFunds(CarPoolOpportunity query);
    Task<CarPoolOpportunity> GetFund(Guid FundId);
    Task<CarPoolOpportunity> GetFundDetails(Guid fundId);
    #endregion

    #region [ FundQueries]
   // Task<IEnumerable<FundQueries>> GetAllFundQueries();
    #endregion

    #region [ Advisors ]
    Task<Advisors> GetAdvisor(Guid advisorId);
    Task<IEnumerable<Advisors>> GetAllAdvisors();

    #endregion
}

