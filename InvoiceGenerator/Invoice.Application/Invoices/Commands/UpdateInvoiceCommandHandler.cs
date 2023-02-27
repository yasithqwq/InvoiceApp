using AutoMapper;
using Invoice.Application.Invoices.Dtos;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Domain.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Commands
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, UpdateInvoiceItemDto>
    {
        private readonly IInvoiceItemRepository _repo;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(IInvoiceItemRepository repo, IMapper mapper)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;
        }
        public async Task<UpdateInvoiceItemDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UpdateInvoiceItemDto, InvoiceItem>(request.InvoiceItemDto);
            InvoiceItem entityToUpdate = await _repo.GetItemAsync(entity.Id);
            if (entityToUpdate == null)
            {
                throw new EntityNotFoundException(nameof(entity), entity.Id);
            }
            entityToUpdate.Description = entity.Description;
            entityToUpdate.TotalAmount = entity.TotalAmount;
            entityToUpdate.InvoiceLines = new List<InvoiceLine>();

            decimal totalAmount = 0;
            foreach (InvoiceLine lineItem in entity.InvoiceLines)
            {
                InvoiceLine itemLine = new InvoiceLine();
                itemLine.Quantity = lineItem.Quantity;
                itemLine.UnitPrice = lineItem.UnitPrice;
                itemLine.LineAmount = itemLine.Quantity * itemLine.UnitPrice;
                itemLine.Amount = itemLine.Quantity * itemLine.UnitPrice;
                entityToUpdate.InvoiceLines.Add(itemLine);
                totalAmount += itemLine.Amount;
            }
            entityToUpdate.TotalAmount = totalAmount;
            await _repo.UpdateItemAsync(entityToUpdate.Id, entityToUpdate);

            var updatedEntry = await _repo.GetItemAsync(entity.Id);
            var updatedDto = _mapper.Map<InvoiceItem, UpdateInvoiceItemDto>(updatedEntry);
            return updatedDto;
        }
    }
}
