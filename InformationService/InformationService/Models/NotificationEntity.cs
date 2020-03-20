using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace InformationService.Models
{
    public class NotificationEntity : TableEntity
    {
        public NotificationEntity(string type, string name)
        {
            this.PartitionKey = type;
            this.RowKey = name;
        }
        public NotificationEntity()
        {
            
        }

        public string Message { get; set; }
        public string Year { get; set; }
        public long SportId { get; set; }
        public long ProgramId { get; set; }
    }
}
