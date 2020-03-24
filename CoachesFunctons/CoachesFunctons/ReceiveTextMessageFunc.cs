using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using TrainingManagingWorker;
using TrainingNotificationWorker;
using Twilio.TwiML;

namespace CoachesFunctons
{
    public static class ReceiveTextMessageFunc
    {
        [FunctionName("ReceiveTextMessageFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string from = req.Query["From"];
            log.LogInformation(req.ToString());

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            var formValues = requestBody.Split('&')
                .Select(value => value.Split('='))
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "),
                    pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));
            log.LogInformation(formValues["From"]);
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //from = from ?? data?.from;
            EventTextDto dto = new EventTextDto();
            dto.Message = formValues["Body"];
            dto.From = formValues["From"];
            dto.Zip = formValues["FromZip"];
            dto.City = formValues["FromCity"];
            string responseMessage = $"Hello, {formValues["Body"]}. This message is from the PWSO Notification System.";

            try
            {
                var connectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var azureConnectionString = System.Environment.GetEnvironmentVariable("AzureStorageConnectionString");
                var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;
                PwsodbContext context = new PwsodbContext(options);
                ITrainingRepository trainingRepository = new TrainingRepository(context);
                IRefRepository refRepository = new RefRepository(azureConnectionString);
                RegistrantWorker worker = new RegistrantWorker(trainingRepository, refRepository);
                var notification  = worker.AddNumberForEvent(dto);
                responseMessage = notification.Result.Message;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //string smsinfo = data.ToString();
            //log.LogInformation(smsinfo);


            //var data = await req.Content.ReadAsStringAsync();
            //var formValues = data.Split('&')
            //    .Select(value => value.Split('='))
            //    .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "),
            //        pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));

            //// Perform calculations, API lookups, etc. here

            //var response = new MessagingResponse()
            //    .Message($"You said: {formValues["Body"]}");
            //var twiml = response.ToString();
            //twiml = twiml.Replace("utf-16", "utf-8");

            //return new HttpResponseMessage
            //{
            //    Content = new StringContent(twiml, Encoding.UTF8, "application/xml")
            //};
            responseMessage += ". This message is from the PWSO Notification System.";
            return new OkObjectResult(responseMessage);
        }
    }
}
