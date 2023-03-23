using System;
using CoMute.Abstractions;
using CoMute.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMute.DataProvider
{
	public partial class PortalDataProvider : IPortalDataProvider
    {
		public PortalDataProvider(IDbContextFactory<DataContext> factory)
		{
			this._contextFactory = factory;
		}

		protected readonly IDbContextFactory<DataContext> _contextFactory;

		public void Initialize()
		{
			using(var context = _contextFactory.CreateDbContext())
			{
				context.Database.EnsureCreated();
			}
		}

		public async Task<T> Get<T>(params object[] keyValues) where T : class
		{
			var result = default(T);
			using (var context = await this._contextFactory.CreateDbContextAsync())
			{
				result = await context.FindAsync<T>(keyValues);
			}

			return result;
		}

		public async Task Insert<T>(T entity)
		{
			using (var context = await this._contextFactory.CreateDbContextAsync())
			{
				await context.AddAsync(entity);
				await context.SaveChangesAsync();
			}
		}

		public async Task Update<T>(T entity)
		{
			using (var context = await this._contextFactory.CreateDbContextAsync())
			{
			    context.Update(entity);
				await context.SaveChangesAsync();
			}
		}
	}
}

