using Invoice.Application.Invoices.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Invoice.Domain.Interfaces.Persistence;
using Invoice.Domain.Entities;

namespace Invoice.Application.Invoices.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceItemDto>
    {
        private readonly IInvoiceItemRepository _repo;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IInvoiceItemRepository repo, IMapper mapper)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;
        }
        public async Task<InvoiceItemDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<InvoiceItemDto, InvoiceItem>(request.InvoiceItemDto);
            await _repo.AddItemAsync(entity);
            var addedEntry = await _repo.GetItemsAsyncByDescription(entity.Description);
            var addedDto = _mapper.Map<InvoiceItem, InvoiceItemDto>(addedEntry.First());
            return addedDto;
        }
    }
}
