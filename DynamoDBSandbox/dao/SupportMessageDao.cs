using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DynamoDBSandbox.domain;

namespace DynamoDBSandbox.dao
{
    public class SupportMessageDao
    {
        private readonly DynamoDBContext context;

        public SupportMessageDao(DynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<SupportMessage> Put(SupportMessage message)
        {
            await context.SaveAsync(message);
            return message;
        }

        public async Task<SupportMessage> Get(string department, string time)
        {
            return await context.LoadAsync<SupportMessage>(department, time);
        }

        public async Task Delete(string department, DateTime time)
        {
            var message = new SupportMessage {Department = department, Time = time};
            await context.DeleteAsync(message);
        }

        public async Task<List<SupportMessage>> GetAllAsync()
        {
            return await context.ScanAsync<SupportMessage>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<List<SupportMessage>> GetAllForDepartment(string department)
        {
            return await context.QueryAsync<SupportMessage>(department).GetRemainingAsync();
        }

        public async Task<List<SupportMessage>> GetAllForDepartmentOnDay(string department, DateTime day)
        {
            return await context.QueryAsync<SupportMessage>(department, QueryOperator.Between, new List<object>{day, day.AddDays(1)}).GetRemainingAsync();
        }
    }
}
