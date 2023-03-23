using System;
using AeverPortal.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace AeverPortal.DataProvider.CosmosDb;

public partial class PortalDataProvider
{
    public async Task<IEnumerable<CarPoolOpportunity>> GetAllFunds()
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Funds.ToListAsync();

            return results;
        }
    }

    public async Task<CarPoolOpportunity> GetFund(Guid FundId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Funds.FindAsync(FundId);
            //await context.Users.LoadAsync();

            return results;
        }
    }

    public async Task<CarPoolOpportunity> GetFundDetails(Guid fundId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Funds.FindAsync(fundId);
            await context.InvestmentTeamMember.LoadAsync();

            return results;
        }
    }

    public async Task<IEnumerable<CarPoolOpportunity>> QueryFunds(CarPoolOpportunity query)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Funds.Where(f => f.Name.ToLower().Contains(query.Name.ToLower())
                                                        || f.Type.ToLower().Contains(query.Type.ToLower())
                                                        || f.Status.ToLower().Contains(query.Status.ToLower())
                                                        || f.InvestmentStrategy.ToLower().Contains(query.InvestmentStrategy.ToLower())
                                                        || f.ValueCreation.ToLower().Contains(query.ValueCreation.ToLower())).ToListAsync();
            return results;
        }
    }
}

