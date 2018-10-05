
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
using NMCT.IoTCloud._MyFirstServices.Model;

namespace NMCT.IoTCloud._MyFirstServices
{
    public static class PostCalc
    {
        [FunctionName("PostCalc")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Calc data = JsonConvert.DeserializeObject<Calc>(requestBody);
            Result r = new Result { Som = data.Getal1 + data.Getal2 };
            return new OkObjectResult(r);
        }
    }
}
