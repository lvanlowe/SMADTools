using System;
using System.IO;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using TrainingManagingWorker;
using TrainingNotificationWorker;

namespace CoachesFunctons
{
    public static class SendCancelationForPracticeFunc
    {
        [FunctionName("SendCancelationForPracticeFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function for cancelation practice.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic sportEmail = JsonConvert.DeserializeObject<CancelEventDto>(requestBody);


            try
            {
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;
                PwsodbContext context = new PwsodbContext(options);
                ICalendarRepository calendarRepository = new CalendarRepository(context);
                IReferenceRepository referenceRepository = new ReferenceRepository(context);
                CalendarWorker worker = new CalendarWorker(calendarRepository, referenceRepository);
                CoachEmailDto cancelEmail = await worker.ProcessEventCancelation(sportEmail);

                var apiKey = System.Environment.GetEnvironmentVariable("ApiKey");
                IEmailRepository emailRepository = new EmailRepository(apiKey);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                EmailWorker emailWorker = new EmailWorker(trainingRepository, emailRepository);
                var numOfEmails = await emailWorker.SendEmailsForSport(cancelEmail);
                CoachTextDto cancelMessage = new CoachTextDto
                {
                    Message = cancelEmail.Subject,
                    ProgramId = cancelEmail.ProgramId,
                    SportId = cancelEmail.SportId,
                };

                var accountSid = System.Environment.GetEnvironmentVariable("AccountSid");
                var authToken = System.Environment.GetEnvironmentVariable("AuthToken");
                var fromPhone = System.Environment.GetEnvironmentVariable("FromPhone");
                ISmsRepository smsRepository = new SmsRepository(accountSid, authToken, fromPhone);
                var textWorker = new TextWorker(trainingRepository, smsRepository);
                var numOfSms = await textWorker.SendSmsForSport(cancelMessage);

                return (ActionResult)new OkObjectResult(numOfEmails);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }
        }
    }
}
