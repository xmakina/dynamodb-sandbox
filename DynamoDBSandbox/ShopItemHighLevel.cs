using System;
using Amazon.DynamoDBv2;
using DynamoDBSandbox.dao;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox
{
    public class ShopItemHighLevel
    {
        public static void Run(AmazonDynamoDBClient client)
        {
            var shopItemDao = new ShopItemDao(client);

            var item1 = shopItemDao.Put(new ShopItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lawn mower",
                Description = "The very best",
                Amount = 4
            }).Result;

            var item2 = shopItemDao.Put(new ShopItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Smart TV",
                Description = "New model",
                Amount = 5
            }).Result;

            var item3 = shopItemDao.Put(new ShopItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Laptop",
                Description = "Powerful gaming laptop",
                Amount = 10,
                Address = new Address{houseNumber = "11", streetName = "High Street"},
                dateTime = DateTime.Now
            }).Result;

            Console.WriteLine("All items:");
            Utils.Print(shopItemDao.GetAllAsync().Result);
            Utils.Pause();

            Console.WriteLine();
            shopItemDao.Delete(item2.Id).Wait();
            Console.WriteLine("After deletion");
            Utils.Print(shopItemDao.GetAllAsync().Result);
        }
    }
}
