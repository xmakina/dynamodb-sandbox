using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDBSandbox.domain;
using static System.Int32;

namespace DynamoDBSandbox.dao.lowLevel
{
    public class ItemDao
    {
        private readonly AmazonDynamoDBClient dynamoDb;

        public ItemDao(AmazonDynamoDBClient dynamoDb)
        {
            this.dynamoDb = dynamoDb;
        }

        public async Task Put(ShopItem item)
        {
            var itemMap = new Dictionary<string, AttributeValue>
            {
                {"id", new AttributeValue{S = item.Id}},
                {"name", new AttributeValue{S = item.Name}},
                {"description", new AttributeValue{S=item.Description}},
                {"amount", new AttributeValue {N = item.Amount.ToString()}}
            };

            var request = new PutItemRequest("ShopItem", itemMap);
            
            await dynamoDb.PutItemAsync(request);
        }

        public async Task<ShopItem> Get(string id)
        {
            var itemKey = new Dictionary<string, AttributeValue>
            {
                {"id", new AttributeValue{S = id}}
            };

            var request = new GetItemRequest("ShopItem", itemKey, true);

            var getItemResponse = await dynamoDb.GetItemAsync(request);
            var dynamoDbItem = getItemResponse.Item;

            var result = new ShopItem
            {
                Id = dynamoDbItem["id"].S,
                Name = dynamoDbItem["name"].S,
                Description = dynamoDbItem["description"].S,
                Amount = Parse(dynamoDbItem["amount"].N)
            };

            return result;
        }
    }
}
