using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox
{
    public static class Utils
    {
        public static void Print<T>(List<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public static void Pause()
        {
            Console.ReadLine();
        }

        public static async Task CreateShopItemTable(AmazonDynamoDBClient client)
        {
            var request = ShopItemTableRequest();

            await CheckAndCreateTable(client, request);
        }

        public static async Task CreateSupportMessageTable(AmazonDynamoDBClient client)
        {
            var request = SupportMessageTableRequest();

            await CheckAndCreateTable(client, request);
        }

        private static string GetTableName<T>()
        {
            var dynamoDbTableAttribute = (DynamoDBTableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DynamoDBTableAttribute));
            if (dynamoDbTableAttribute == null)
            {
                throw new Exception("No DynamoDBTable attribute found!");
            }
            
            return dynamoDbTableAttribute.TableName;
        }

        private static async Task CheckAndCreateTable(IAmazonDynamoDB client, CreateTableRequest request)
        {
            if (await TableExists(client, request.TableName))
            {
                Console.WriteLine($"The table {request.TableName} already exists");
                return;
            }

            await client.CreateTableAsync(request);
            await WaitForTableCreated(request.TableName, client);
        }

        private static CreateTableRequest ShopItemTableRequest()
        {
            var tableName = GetTableName<ShopItem>();
            return new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions =
                    new List<AttributeDefinition>
                    {
                        new AttributeDefinition {AttributeName = "id", AttributeType = ScalarAttributeType.S}
                    },
                ProvisionedThroughput = new ProvisionedThroughput { WriteCapacityUnits = 1, ReadCapacityUnits = 1 },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement {AttributeName = "id", KeyType = KeyType.HASH}
                }
            };
        }

        private static CreateTableRequest SupportMessageTableRequest()
        {
            var tableName = GetTableName<SupportMessage>();
            return new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions =
                    new List<AttributeDefinition>
                    {
                        new AttributeDefinition {AttributeName = "department", AttributeType = ScalarAttributeType.S},
                        new AttributeDefinition{AttributeName = "time", AttributeType = ScalarAttributeType.S }
                    },
                ProvisionedThroughput = new ProvisionedThroughput { WriteCapacityUnits = 1, ReadCapacityUnits = 1 },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement {AttributeName = "department", KeyType = KeyType.HASH},
                    new KeySchemaElement{AttributeName = "time", KeyType = KeyType.RANGE}
                }
            };
        }

        private static async Task WaitForTableCreated(string tableName, IAmazonDynamoDB dynamoDB)
        {
            while (true)
            {
                try
                {
                    var result = await dynamoDB.DescribeTableAsync(tableName);
                    var table = result.Table;

                    if (table == null || table.TableStatus != TableStatus.ACTIVE)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }

                    Console.WriteLine("Table created and active!");
                    return;
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("Table still not created. Waiting...");
                }
            }
        }

        private static async Task<bool> TableExists(IAmazonDynamoDB client, string tableName)
        {
            try
            {
                await client.DescribeTableAsync(tableName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
