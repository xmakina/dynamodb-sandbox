using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBSandbox.domain
{
    public class SupportAgent
    {
        [DynamoDBProperty("employee-id")]
        public string EmployeeID;

        [DynamoDBProperty("name")]
        public string Name;

        public override string ToString()
        {
            return "SupportMessage: {" +
                   $"employee-id='{EmployeeID}', " +
                   $"name='{Name}'" +
                   "}";

        }
    }
}
