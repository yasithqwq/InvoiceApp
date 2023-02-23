using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.CosmosDbData.Constants
{
    public class CosmosDbContainerConstants
    {
        // TODO : consider retrieving this from appsettings using IOptions, instead of defining it as a constant
        public const string CONTAINER_NAME_Invoices = "Invoices";
    }
}
