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
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, InvoiceItemDto>
    {
        private readonly IInvoiceItemRepository _repo;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(IInvoiceItemRepository repo, IMapper mapper)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;
        }
        public async Task<InvoiceItemDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<InvoiceItemDto, InvoiceItem>(request.InvoiceItemDto);
            InvoiceItem entityToUpdate = await _repo.GetItemAsync(entity.Id);
            if (entityToUpdate == null)
            {
                throw new EntityNotFoundException(nameof(entity), entity.Id);
            }

            await _repo.UpdateItemAsync(entity.Id, entity);

            var updatedEntry = await _repo.GetItemAsync(entity.Id);
            var updatedDto = _mapper.Map<InvoiceItem, InvoiceItemDto>(updatedEntry);
            return updatedDto;
        }
    }
}
