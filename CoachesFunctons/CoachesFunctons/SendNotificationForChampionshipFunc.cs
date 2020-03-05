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
    public static class SendNotificationForChampionshipFunc
    {
        [FunctionName("SendNotificationForChampionshipFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic teams = JsonConvert.DeserializeObject<TournamentDto>(requestBody);


            try
            {
                var apiKey = System.Environment.GetEnvironmentVariable("ApiKey");
                IEmailRepository emailRepository = new EmailRepository(apiKey);
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;
                PwsodbContext context = new PwsodbContext(options);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                IReferenceRepository referenceRepository = new ReferenceRepository(context);
                ReferenceWorker refWorker = new ReferenceWorker(referenceRepository);
                EmailWorker emailWorker = new EmailWorker(trainingRepository, emailRepository);
                var accountSid = System.Environment.GetEnvironmentVariable("AccountSid");
                var authToken = System.Environment.GetEnvironmentVariable("AuthToken");
                var fromPhone = System.Environment.GetEnvironmentVariable("FromPhone");
                ISmsRepository smsRepository = new SmsRepository(accountSid, authToken, fromPhone);
                var textWorker = new TextWorker(trainingRepository, smsRepository);

                foreach (var team in teams.Teams)
                {
                    var emailMessage = refWorker.ChampionshipEmailPreparation(team);
                    var textMessage = refWorker.ChampionshipTextPreparation(team);
                    var numOfEmails = await emailWorker.SendEmailsForSport(emailMessage);
                    var numOfSms = await textWorker.SendSmsForSport(textMessage);

                }

                return (ActionResult)new OkObjectResult(1);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }
        }
    }
}
