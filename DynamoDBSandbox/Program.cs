
using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using DynamoDBSandbox.dao;

namespace DynamoDBSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = new BasicAWSCredentials("AKIASHNHXRBWMDNXDZHG", "Su3g3F0v3+GJiR6U40eav0POXYSjds6DANsxM0Hg");
            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

            PopulateQueryMessages.Run(client);
            QuerySupportMessages.Run(client);
        }
    }

    internal class QuerySupportMessages
    {
        public static void Run(AmazonDynamoDBClient client)
        {
            var dao = new SupportMessageDao(new DynamoDBContext(client));
            var results = dao.GetAllForDepartment("business").Result;
            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
            }

            var dayResults = dao.GetAllForDepartmentOnDay("delivery", new DateTime(2019, 11, 10)).Result;
            foreach (var result in dayResults)
            {
                Console.WriteLine(result.ToString());
            }
        }
    }
}
