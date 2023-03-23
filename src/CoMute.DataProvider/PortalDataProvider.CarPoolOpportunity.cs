using System;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider;

public partial class PortalDataProvider
{
    public async Task<IEnumerable<CarPoolOpportunity>> GetAllCarPoolOpportunities()
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.CarPoolOpportunities.ToListAsync();

            return results;
        }
    }

    public async Task<CarPoolOpportunity> GetCarPoolOpportunity(Guid carPoolOpportunityId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.CarPoolOpportunities.FindAsync(carPoolOpportunityId);

            return results;
        }
    }
}

