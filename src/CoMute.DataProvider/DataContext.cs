using System;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider;

public class DataContext : DbContext
{
	public const string PartitionKey = nameof(PartitionKey);

	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
	}

	public DbSet<CarPoolOpportunity> CarPoolOpportunities { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		#region [ User ]
		var userModel = modelBuilder.Entity<User>();
		userModel.ToTable("User")
			.HasDiscriminator<int>("Type")
			.HasValue<User>(0);

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

