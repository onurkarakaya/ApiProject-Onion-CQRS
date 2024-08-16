using ApiProject.Application.Interfaces.Repositories;
using ApiProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
	{
		private readonly DbContext _dbContext;

		public ReadRepository(DbContext dbContext) 
		{
			_dbContext = dbContext;
		}

        private DbSet<T> _dbSet { get => _dbContext.Set<T>(); }

		public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
		{
			IQueryable<T> queryable = _dbSet;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);
			if (predicate is not null) queryable = queryable.Where(predicate);
			if (orderBy is not null)
				return await orderBy(queryable).ToListAsync();

			return await queryable.ToListAsync();
		}

		public async Task<IList<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
		{
			IQueryable<T> queryable = _dbSet;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);
			if (predicate is not null) queryable = queryable.Where(predicate);
			if (orderBy is not null)
				return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

			return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
		{
			IQueryable<T> queryable = _dbSet;
			if (!enableTracking) queryable = queryable.AsNoTracking();
			if (include is not null) queryable = include(queryable);

			//queryable.Where(predicate);

			return await queryable.FirstOrDefaultAsync(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
		{
			_dbSet.AsNoTracking();

			if (predicate is not null) _dbSet.Where(predicate);

			return await _dbSet.CountAsync();
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
		{
			if (!enableTracking) _dbSet.AsNoTracking();

			return _dbSet.Where(predicate);
		}	
	}
}
