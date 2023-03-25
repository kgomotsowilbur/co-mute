using System;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider;

public partial class PortalDataProvider
{
    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.ToListAsync();

            return results;
        }
    }

    public async Task<Users> GetUser(string userId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.FindAsync(userId);
            return results;
        }
    }

    public async Task<Users> GetUserDetails(string userId)
    {
        using (var context = await _contextFactory.CreateDbContextAsync())
        {
            var results = await context.Users.FindAsync(userId);
            await context.CarPoolOpportunities.LoadAsync();

            return results;
        }
    }
}