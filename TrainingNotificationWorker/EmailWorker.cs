using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;

namespace TrainingNotificationWorker
{
    public class EmailWorker
    {

        private PwsodbContext _context;

        public EmailWorker(string connectionString)
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;

            _context = new PwsodbContext(options);
        }


        public async Task<List<SportEmails>> GetEmailsForSport(int sportId)
        {
            throw new NotImplementedException();
        }
    }
}
