using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Infrastructure.CosmosDb.Queries
{
    public interface ICosmosQuery<T>
    {
        public T Execute(CosmosClient client);
    }
}
