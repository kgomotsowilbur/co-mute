using System;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider.CosmosDb;

public class DataContext : DbContext
{
	public const string PartitionKey = nameof(PartitionKey);

	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
		// this.ChangeTracker.AutoDetectChangesEnabled = false;
	}

	public DbSet<CarPoolOpportunity> CarPoolOpportunities { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		#region [ User ]
		var userModel = modelBuilder.Entity<User>();
		userModel.ToContainer("User")
            .HasNoDiscriminator()
            .HasKey(w => w.Id);

		userModel.HasMany(a => a.CarPoolOpportunities)
			.WithOne()
			.HasForeignKey(t => t.UserId);
		#endregion
		#region [ CarPoolOpportunity ]
		var fundModel = modelBuilder.Entity<CarPoolOpportunity>();
		fundModel.ToContainer("CarPoolOpportunities")
            .HasNoDiscriminator()
            .HasKey(f => f.Id);
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

