using BookstoreApp.Application.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Service
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");
            }

            var connectionString = configuration["Redis:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Redis connection string cannot be null or empty.", nameof(configuration));
            }

            var connection = ConnectionMultiplexer.Connect(connectionString);
            _db = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string key)
            => await _db.StringGetAsync(key);

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
            => await _db.StringSetAsync(key, value, expiry);

        public async Task RemoveAsync(string key)
            => await _db.KeyDeleteAsync(key);

        public async Task<List<string>> GetListAsync(string key)
        {
            var result = await _db.ListRangeAsync(key);
            return result.Select(x => x.ToString()).ToList();
        }

        public async Task PushToListAsync(string key, string value, int maxLength = 10)
        {
            await _db.ListLeftPushAsync(key, value);
            await _db.ListTrimAsync(key, 0, maxLength - 1);
        }
    }
}
