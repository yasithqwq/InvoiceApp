using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.CosmosDbData.Interfaces
{
    public interface ICosmosDbSeed
    {
        Task InvoiceItemSeed();
    }
}
