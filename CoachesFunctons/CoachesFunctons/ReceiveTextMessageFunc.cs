using System;
using System.IO;
using System.Linq;
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
using TrainingManagingWorker;

namespace CoachesFunctons
{
    public static class ReceiveTextMessageFunc
    {
        [FunctionName("ReceiveTextMessageFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# PWSO receive sms message process.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            var formValues = requestBody.Split('&')
                .Select(value => value.Split('='))
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "),
                    pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));
            var dto = new EventTextDto
            {
                Message = formValues["Body"],
                From = formValues["From"],
                Zip = formValues["FromZip"],
                City = formValues["FromCity"]
            };
            //var dto = new EventTextDto{Message = "Track", From = "17035901821", Zip = "22193", City = "Dale City"} ;
            string responseMessage;

            try
            {
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var azureConnectionString = System.Environment.GetEnvironmentVariable("AzureStorageConnectionString");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString ?? throw new InvalidOperationException()).Options;
                var context = new PwsodbContext(options);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                IRefRepository refRepository = new RefRepository(azureConnectionString);
                var worker = new RegistrantWorker(trainingRepository, refRepository);
                var notification  = worker.AddNumberForEvent(dto);
                responseMessage = notification.Result.Message;
            }
            catch (Exception e)
            {
                log.LogError("Fatal Error: " + e.ToString());

                throw;
            }

            responseMessage += ". This message is from the PWSO Notification System.";
            return new OkObjectResult(responseMessage);
        }
    }
}
