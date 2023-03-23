using System;
using AeverPortal.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace AeverPortal.DataProvider.CosmosDb;

public class DataContext : DbContext
{
	public const string PartitionKey = nameof(PartitionKey);

	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
		// this.ChangeTracker.AutoDetectChangesEnabled = false;
	}

	public DbSet<CarPoolOpportunity> Funds { get; set; }
    public DbSet<Advisors> Advisors { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
	public DbSet<AboutUs> AboutUs { get; set; }
	public DbSet<User> FundManagers { get; set; }
	public DbSet<InvestmentTeamMember> InvestmentTeamMember { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		#region [ TeamMember ]
		var teamMemberModel = modelBuilder.Entity<TeamMember>();
		teamMemberModel.ToContainer("TeamMember")
            .HasNoDiscriminator()
            .HasKey(w => w.Id);

		teamMemberModel.HasOne(u => u.SocialMedia);
		teamMemberModel.HasOne(u => u.PhysicalAddress);
		teamMemberModel.HasOne(u => u.RegisteredAddress);
		#endregion
		#region [ FundManager ]
		var fundManagerModel = modelBuilder.Entity<User>();
		fundManagerModel.ToContainer("FundManager")
            .HasNoDiscriminator()
            .HasKey(w => w.Id);

		fundManagerModel.HasMany(u => u.TeamMembers)
			.WithOne()
			.HasForeignKey(t => t.FundManagerId);
		fundManagerModel.HasMany(u => u.Advisors)
			.WithOne()
            .HasForeignKey(t => t.FundManagerId);
		fundManagerModel.HasMany(a => a.Funds)
			.WithOne()
			.HasForeignKey(t => t.FundManagerId);

		fundManagerModel.OwnsMany(u => u.AboutUs);
		fundManagerModel.OwnsOne(u => u.RegisteredAddress);
		fundManagerModel.OwnsOne(u => u.PhysicalAddress);
		#endregion
		#region [ Fund ]
		var fundModel = modelBuilder.Entity<CarPoolOpportunity>();
		fundModel.ToContainer("Funds")
            .HasNoDiscriminator()
            .HasKey(f => f.Id);

		fundModel.HasMany(u => u.InvestmentTeamMember)
			.WithOne()
			.HasForeignKey(t => t.FundId);

		fundModel.OwnsOne(u => u.Investment);
		fundModel.OwnsOne(u => u.InvestmentTerm);
		fundModel.OwnsOne(u => u.PhysicalAddress);
		fundModel.OwnsOne(u => u.RegisteredAddress);
		#endregion
		#region [ Funding Tareget ]
		//var targetModel = modelBuilder.Entity<FundingTarget>();
		//targetModel.ToContainer("FundTargets")
		//	.HasNoDiscriminator()
		//	.HasKey(t => t.Id);
		#endregion

		#region [ Advisors ]
		var advisorsModel = modelBuilder.Entity<Advisors>();
		advisorsModel.ToContainer("Advisors")
			.HasNoDiscriminator()
			.HasKey(f => f.Id);
		#endregion

		#region [ InvestmentTeamMember ]
		var investmentTeamMemberModel = modelBuilder.Entity<InvestmentTeamMember>();
		investmentTeamMemberModel.ToContainer("InvestmentTeamMember")
		.HasNoDiscriminator()
		.HasKey(f => f.Id);

		investmentTeamMemberModel.OwnsOne(u => u.SocialMedia);
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

