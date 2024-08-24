using ApiProject.Application.Interfaces.Repositories;
using ApiProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
	{
		private readonly DbContext _dbContext;

		public WriteRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		private DbSet<T> _dbSet { get => _dbContext.Set<T>(); }

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task AddRangeAsync(IList<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
		}

		public async Task<T> UpdateAsync(T entity)
		{
			await Task.Run(() => _dbSet.Update(entity));
			return entity;
		}

		public async Task HardDeleteAsync(T entity)
		{
			await Task.Run(() => _dbSet.Remove(entity));
		}

		public async Task HardDeleteRangeAsync(IList<T> entities)
		{
			await Task.Run(() => _dbSet.RemoveRange(entities));
		}
	}
}
