using System;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBSandbox.domain
{
    [DynamoDBTable("SupportMessages")]
    public class SupportMessage
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("department")]
        public string Department;
        
        [DynamoDBRangeKey]
        [DynamoDBProperty("time")]
        public DateTime Time;

        [DynamoDBProperty("urgency")]
        public int Urgency;

        [DynamoDBProperty("message")]
        public string Message;

        [DynamoDBProperty("support-agent")]
        public SupportAgent SupportAgent;

        public SupportMessage(){}

        public SupportMessage(string department, DateTime time, int urgency, string message, SupportAgent supportAgent)
        {
            Department = department;
            Time = time;
            Urgency = urgency;
            Message = message;
            SupportAgent = supportAgent;
        }

        public override string ToString()
        {
            return "SupportMessage: {" +
                   $"department='{Department}', " +
                   $"time='{Time}', " +
                   $"urgency='{Urgency}', " +
                   $"message='{Message}'" +
                   $"Agent={{{SupportAgent}}}'" +
                   "}";

        }
    }
}
