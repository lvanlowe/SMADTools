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
using TrainingNotificationWorker;

namespace CoachesFunctons
{
    public static class SendTextForSportFunc
    {
        [FunctionName("SendTextForSportFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic sportMessage = JsonConvert.DeserializeObject<CoachTextDto>(requestBody);


            try
            {
                var accountSid = System.Environment.GetEnvironmentVariable("AccountSid");
                var authToken = System.Environment.GetEnvironmentVariable("AuthToken");
                var fromPhone = System.Environment.GetEnvironmentVariable("FromPhone");
                ISmsRepository smsRepository = new SmsRepository(accountSid,authToken,fromPhone);
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString ?? throw new InvalidOperationException()).Options;
                var context = new PwsodbContext(options);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                var worker = new TextWorker(trainingRepository, smsRepository);
                var numOfSms = await worker.SendSmsForSport(sportMessage);
                return (ActionResult)new OkObjectResult(numOfSms);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }

        }
    }
}
