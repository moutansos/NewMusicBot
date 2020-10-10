using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewMusicBot.Infrastructure.CosmosDb.Command
{
    public interface ICosmosCommand<T>
    {
        T Execute(CosmosClient client);
    }
}
