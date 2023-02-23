using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Invoice.Domain.Interfaces.Persistence;
using System.Collections.Generic;
using Invoice.Infrastructure.CosmosDbData;
using Invoice.Infrastructure.CosmosDbData.Interfaces;

namespace Invoice.Api
{
    public class CreateInvoice
    {
        private readonly ICosmosDbSeed _seed;

        public CreateInvoice(ICosmosDbSeed seed)
        {
            this._seed = seed ?? throw new ArgumentNullException(nameof(seed));
        }

        [FunctionName("CreateInvoiceWithInvoiceLines")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            await _seed.InvoiceItemSeed();

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
