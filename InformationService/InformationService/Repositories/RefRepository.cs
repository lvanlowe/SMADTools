using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace InformationService.Repositories
{
    public class RefRepository : IRefRepository
    {
        private string _connectionString;

        public RefRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<NotificationEntity> GetEventByName(string eventName)
        {
            var notification = new NotificationEntity();
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var notificationTable = tableClient.GetTableReference("Notification");
            var retrieveOperation = TableOperation.Retrieve<NotificationEntity>("Event", eventName);
            var retrievedResult = await notificationTable.ExecuteAsync(retrieveOperation);
            if (retrievedResult.Result != null)
                notification = (NotificationEntity)retrievedResult.Result;
            return notification;
        }
    }
}
