using System;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider;

public partial class PortalDataProvider
{
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.ToListAsync();

            return results;
        }
    }

    public async Task<User> GetUser(Guid userId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.FindAsync(userId);
            return results;
        }
    }

    public async Task<User> GetUserDetails(Guid userId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.FindAsync(userId);
            await context.CarPoolOpportunities.LoadAsync();

            return results;
        }
    }
}