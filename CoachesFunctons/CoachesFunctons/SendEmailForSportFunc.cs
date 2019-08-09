using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using InterfaceModels;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using InformationService.Interfaces;
using InformationService.Repositories;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;
using TrainingNotificationWorker;

namespace CoachesFunctons
{
    public static class SendEmailForSportFunc
    {
        [FunctionName("SendEmailForSportFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic sportEmail = JsonConvert.DeserializeObject<CoachEmailDto>(requestBody);


            try
            {
                var apiKey = System.Environment.GetEnvironmentVariable("ApiKey");
                IEmailRepository emailRepository = new EmailRepository(apiKey);
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZUCONNSTR_TrainingModel");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;
                PwsodbContext context = new PwsodbContext(options);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                EmailWorker worker = new EmailWorker(trainingRepository, emailRepository);
                var numOfEmails = await worker.SendEmailsForSport(sportEmail);
                return (ActionResult)new OkObjectResult(numOfEmails);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }
                
        }
    }
}
