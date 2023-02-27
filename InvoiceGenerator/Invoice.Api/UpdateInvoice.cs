using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Invoice.Application.Invoices.Commands;
using Invoice.Application.Invoices.Dtos;
using Invoice.Infrastructure.CosmosDbData.Interfaces;
using MediatR;
using Microsoft.Azure.Cosmos;

namespace Invoice.Api
{
    public class UpdateInvoice
    {
        private readonly IMediator _mediator;

        public UpdateInvoice(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [FunctionName("UpdateInvoice")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger UpdateInvoice function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UpdateInvoiceItemDto data = JsonConvert.DeserializeObject<UpdateInvoiceItemDto>(requestBody);
                UpdateInvoiceItemDto response = await _mediator.Send(new UpdateInvoiceCommand() { InvoiceItemDto = data });

                string responseMessage = $"UpdateInvoice executed successfully.";

                UpdatedResultDto resultDto = new UpdatedResultDto
                {
                    IsSuccess = true,
                    ResponseMessage = responseMessage,
                    UpdatedObject = response,
                    UpdatedObjectId = response.Id
                };

                return new OkObjectResult(resultDto);
            }
            catch (Exception ex)
            {
                UpdatedResultDto resultDto = new UpdatedResultDto
                {
                    IsSuccess = false,
                    ResponseMessage = ex.Message
                };
                return new BadRequestObjectResult(resultDto);
            }
        }
    }
}
