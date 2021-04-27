using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox.dao
{
    public class ShopItemDao
    {
        private readonly DynamoDBContext context;

        public ShopItemDao(IAmazonDynamoDB client)
        {
            context = new DynamoDBContext(client);
        }

        public async Task<ShopItem> Put(ShopItem item)
        {
            await context.SaveAsync(item);
            return item;
        }

        public async Task<ShopItem> Get(string id)
        {
            return await context.LoadAsync<ShopItem>(id);
        }

        public async Task Delete(string id)
        {
            await context.DeleteAsync<ShopItem>(id);
        }

        public async Task<List<ShopItem>> GetAllAsync()
        {
            return await context.ScanAsync<ShopItem>(new List<ScanCondition>()).GetRemainingAsync();
        }
    }
}
