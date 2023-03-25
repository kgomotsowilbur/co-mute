using System;
using CoMute.Abstractions.Models;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoMute.DataProvider;

public class DataContext : IdentityDbContext<Users>, IPersistedGrantDbContext
{
	public const string PartitionKey = nameof(PartitionKey);
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    public DataContext(DbContextOptions<DataContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
		: base(options)
	{
        _operationalStoreOptions = operationalStoreOptions;
    }

    public DbSet<CarPoolOpportunity> CarPoolOpportunities { get; set; }
	public DbSet<Users> Users { get; set; }

    public DbSet<PersistedGrant> PersistedGrants { get; set; }
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<ServerSideSession> ServerSideSessions { get; set; }
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

        #region [ User ]
        var userModel = modelBuilder.Entity<Users>();
		userModel.ToTable("User")
			.HasDiscriminator<int>("Type")
			.HasValue<Users>(0);

        userModel.HasMany(a => a.CarPoolOpportunities)
			.WithOne()
			.HasForeignKey(t => t.UserId);
		#endregion
		#region [ CarPoolOpportunity ]
		var carPoolOpportunityModel = modelBuilder.Entity<CarPoolOpportunity>();
		carPoolOpportunityModel.ToTable("CarPoolOpportunities")
			.HasDiscriminator<int>("Type")
			.HasValue<CarPoolOpportunity>(1);
        #endregion
    }

#if DEBUG
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		optionsBuilder
			.LogTo(Console.WriteLine)
			.EnableDetailedErrors();
	}
#endif
}

