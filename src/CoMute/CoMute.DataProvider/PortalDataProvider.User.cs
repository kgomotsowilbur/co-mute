using System;
using AeverPortal.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace AeverPortal.DataProvider.CosmosDb;

public partial class PortalDataProvider
{
    public async Task<IEnumerable<User>> GetAllFundManagers()
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.FundManagers.ToListAsync();

            return results;
        }
    }

    public async Task<User> GetFundManager(Guid fundManagerId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.FundManagers.FindAsync(fundManagerId);
            return results;
        }
    }

    public async Task<User> GetFundManagerDetails(Guid fundManagerId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.FundManagers.FindAsync(fundManagerId);
            await context.Funds.LoadAsync();
            await context.InvestmentTeamMember.LoadAsync();
            await context.TeamMembers.LoadAsync();
            await context.Advisors.LoadAsync();

            return results;
        }
    }
}