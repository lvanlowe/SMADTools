using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

            //string smsinfo = data.ToString();
            //log.LogInformation(smsinfo);

            string responseMessage = $"Hello, {formValues["Body"]}. This HTTP triggered function executed successfully.";

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

            return new OkObjectResult(responseMessage);
        }
    }
}
