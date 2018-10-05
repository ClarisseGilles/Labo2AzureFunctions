
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
    public static class Drank
    {
        [FunctionName("Drank")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Drinken data = JsonConvert.DeserializeObject<Drinken>(requestBody);
            if (0 >= data.Leeftijd || data.Leeftijd > 100) return new BadRequestObjectResult("leeftijd moet tussen 0 en 100 zijn");
            Toestemming t = new Toestemming() { MagIk = true };
            if (data.Leeftijd < 18 && (data.Drank == "wijn" || data.Drank == "gin" || data.Drank == "bier")) t.MagIk = false;
            return new OkObjectResult(t);
        }
    }
}
