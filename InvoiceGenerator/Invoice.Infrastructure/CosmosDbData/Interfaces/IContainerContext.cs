using Invoice.Domain.Entities.Base;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.CosmosDbData.Interfaces
{
    /// <summary>
    ///  Defines the container level context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainerContext<T> where T : BaseEntity
    {
        string ContainerName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
