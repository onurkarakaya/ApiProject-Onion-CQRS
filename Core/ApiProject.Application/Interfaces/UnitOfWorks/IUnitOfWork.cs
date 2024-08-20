using ApiProject.Application.Interfaces.Repositories;
using ApiProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Application.Interfaces.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
		IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
		Task<int> SaveAsync();
		int Save();

	}
}
