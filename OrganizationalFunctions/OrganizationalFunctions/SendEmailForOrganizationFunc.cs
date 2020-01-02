using System;
using System.IO;
using System.Threading.Tasks;
using InformationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using InformationService.Models;
using InformationService.Repositories;
using TrainingNotificationWorker;

namespace OrganizationalFunctions
{
    public static class SendEmailForOrganizationFunc
    {
        [FunctionName("SendEmailForOrganizationFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic emailDto = JsonConvert.DeserializeObject<OrganizationEmailDto>(requestBody);


            try
            {
                var apiKey = System.Environment.GetEnvironmentVariable("ApiKey");
                var isTest = System.Environment.GetEnvironmentVariable("IsTest");
                IEmailRepository emailRepository = new EmailRepository(apiKey);
                var organizationConnectionString = System.Environment.GetEnvironmentVariable("SQLCONNSTR_OrganizationModel");
                var organizationOptions = new DbContextOptionsBuilder<PwsoContext>().UseSqlServer(organizationConnectionString ?? throw new InvalidOperationException()).Options;
                var organizationContext = new PwsoContext(organizationOptions);
                IOrganizationRepository organizationRepository = new OrganizationRepository(organizationContext);
                if (isTest == "true")
                {
                    emailDto.IsTesting = true;
                }
                EmailWorker worker = new EmailWorker(organizationRepository, emailRepository);
                var numOfEmails = await worker.SendAdminEmails(emailDto);
                return (ActionResult)new OkObjectResult(numOfEmails);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }

        }
    }
}
