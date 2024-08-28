using ApiProject.Application.Interfaces.RedisCache;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Infrastructure.RedisCache
{
	public class RedisCacheService : IRedisCacheService
	{
		private readonly ConnectionMultiplexer _redisConnection;
		private readonly IDatabase _database;
		private readonly RedisCacheSettings _settings;

		public RedisCacheService(IOptions<RedisCacheSettings> options)
		{
			_settings = options.Value;
			var opt = ConfigurationOptions.Parse(_settings.ConnectionString);
			_redisConnection = ConnectionMultiplexer.Connect(opt);
			_database = _redisConnection.GetDatabase();
		}

		public Task<T> GetAsync<T>(string key)
		{
			throw new NotImplementedException();
		}

		public Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
		{
			throw new NotImplementedException();
		}
	}
}
