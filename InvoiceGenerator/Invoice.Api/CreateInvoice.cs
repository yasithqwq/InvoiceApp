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
using Invoice.Application.Invoices.Dtos;
using MediatR;
using Invoice.Application.Invoices.Commands;

namespace Invoice.Api
{
    public class CreateInvoice
    {
        private readonly ICosmosDbSeed _seed;
        private readonly IMediator _mediator;

        public CreateInvoice(IMediator mediator,ICosmosDbSeed seed)
        {
            _mediator = mediator;
            this._seed = seed ?? throw new ArgumentNullException(nameof(seed));
        }

        [FunctionName("CreateInvoiceWithInvoiceLines")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger CreateInvoiceWithInvoiceLines function processed a request.");

            //Please uncomment this if you need some seed data
            //await _seed.InvoiceItemSeed();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            InvoiceItemDto data = JsonConvert.DeserializeObject<InvoiceItemDto>(requestBody);
            InvoiceItemDto response = await _mediator.Send(new CreateInvoiceCommand() { InvoiceItemDto = data });

            string responseMessage = $"This HTTP triggered function CreateInvoiceWithInvoiceLines executed successfully.\n";
            responseMessage += $"Created Object Id - {response.Id}.";

            return new OkObjectResult(responseMessage);
        }
    }
}
