﻿using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoriesContaract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StackExchange.Redis.IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty? null : JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(customerBasket.Id , JsonSerializer.Serialize(customerBasket) , TimeSpan.FromDays(30));
            if (CreatedOrUpdated is false)
            {
                return null;
            }
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
