using System;
using System.IO;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TrainingManagingWorker;

namespace CoachesFunctons
{
    public static class GetRegistrantsFroSportFunc
    {
        [FunctionName("GetRegistrantsFroSportFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string sport = req.Query["sportid"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            sport = sport ?? data?.name;
            var sportId = int.Parse(sport);
            try
            {

                var trainingConnectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_TrainingModel");
                var organizationConnectionString = System.Environment.GetEnvironmentVariable("SQLAZURECONNSTR_OrganizationModel");
                var trainingOptions = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(trainingConnectionString ?? throw new InvalidOperationException()).Options;
                var organizationOptions = new DbContextOptionsBuilder<PwsoContext>().UseSqlServer(organizationConnectionString ?? throw new InvalidOperationException()).Options;

                var trainingContext = new PwsodbContext(trainingOptions);
                var organizationContext = new PwsoContext(organizationOptions);
                ITrainingRepository trainingRepository = new TrainingRepository(trainingContext);
                IOrganizationRepository organizationRepository = new OrganizationRepository(organizationContext);
                var worker = new RegistrantWorker(trainingRepository, organizationRepository);

                var registrantList = await worker.GetRegistrantsForSport(sportId);
                return (ActionResult)new OkObjectResult(registrantList);

            }
            catch (Exception ex)
            {

                return (ActionResult)new BadRequestObjectResult(ex);
            }
        }
    }
}
