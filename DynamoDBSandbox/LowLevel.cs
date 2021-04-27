using System;
using Amazon.DynamoDBv2;
using DynamoDBSandbox.dao.lowLevel;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox
{
    class LowLevel
    {
        public static void Main(AmazonDynamoDBClient client)
        {
            var itemDao = new ItemDao(client);
            // SaveItem(itemDao);
            // GetItem(itemDao, "1");
        }

        private static void SaveItem(ItemDao itemDao)
        {
            var shopItem = new ShopItem
            {
                Id = "1",
                Name = "Laptop",
                Amount = 5,
                Description = "Powerful gaming laptop"
            };

            itemDao.Put(shopItem).Wait();
            Console.WriteLine("Item was stored!");
        }

        private static void GetItem(ItemDao itemDao, string id)
        {
            var item = itemDao.Get(id).Result;
            Console.WriteLine(item.ToString());
        }
    }
}
