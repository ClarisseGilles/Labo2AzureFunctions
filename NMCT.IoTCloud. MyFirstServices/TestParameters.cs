
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NMCT.IoTCloud._MyFirstServices
{
    public static class TestParameters
    {
        [FunctionName("TestParameters")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string bewerking = req.Query["rekenmachine"];
            string getal1 = req.Query[bewerking];
            string getal2 = req.Query[getal1];

            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            switch (bewerking)
            {
                case "som":
                    return (ActionResult)new OkObjectResult(int.Parse(getal1) + int.Parse(getal2));
                case "delen":
                    return (ActionResult)new OkObjectResult(int.Parse(getal1) / int.Parse(getal2));
                default:
                    return new BadRequestObjectResult("geen bewerking opgegeven");
            }
        }
    }
}
