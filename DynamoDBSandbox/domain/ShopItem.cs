using System;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBSandbox.domain
{
    [DynamoDBTable("ShopItems")]
    public class ShopItem
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("id")]
        public string Id;
        
        [DynamoDBProperty("name")]
        public string Name;
        
        [DynamoDBProperty("description")]
        public string Description;

        [DynamoDBProperty("address")]
        public Address Address;
        
        [DynamoDBProperty("amount")]
        public int Amount;

        [DynamoDBProperty("DateTime")]
        public DateTime dateTime;

        public override string ToString()
        {
            return "ShopItem: {" +
                   $"id='{Id}', " +
                   $"name='{Name}', " +
                   $"description='{Description}', " +
                   $"amount='{Amount}'" +
                   "}";

        }
    }

    public class Address
    {
        [DynamoDBProperty("HouseNumber")]
        public string houseNumber;
        
        [DynamoDBProperty("HouseName")]
        public string streetName;
    }
}
