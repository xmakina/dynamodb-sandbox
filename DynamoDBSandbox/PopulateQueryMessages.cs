using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using DynamoDBSandbox.dao;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox
{
    public class PopulateQueryMessages
    {
        public static void Run(AmazonDynamoDBClient client)
        {
            Utils.CreateSupportMessageTable(client).Wait();
            CreateSupportMessages(client).Wait();
        }

        private static async Task CreateSupportMessages(IAmazonDynamoDB client)
        {
            var messageDao = new SupportMessageDao(new DynamoDBContext(client));
            var john = new SupportAgent{EmployeeID = Guid.NewGuid().ToString(), Name = "John Doe"};
            var jane = new SupportAgent { EmployeeID = Guid.NewGuid().ToString(), Name = "Jane Smith" };

            await messageDao.Put(new SupportMessage(
                "payments", 
                new DateTime(2019, 8, 13, 21, 56, 00), 
                2, 
                "Price mistake on item 345",
                john));

            await messageDao.Put(new SupportMessage(
                "delivery",
                new DateTime(2019, 11, 1, 19, 37, 00),
                2,
                "Items are still missing for order 123",
                john));

            await messageDao.Put(new SupportMessage(
                "delivery",
                new DateTime(2019, 11, 10, 9, 15, 00),
                4,
                "Delivery car broke down",
                jane));

            await messageDao.Put(new SupportMessage(
                "business",
                new DateTime(2019, 10, 25, 16, 20, 00),
                3,
                "Would like to sell those items on your website",
                jane));

            Console.WriteLine("Stored support messages.");
        }
    }
}