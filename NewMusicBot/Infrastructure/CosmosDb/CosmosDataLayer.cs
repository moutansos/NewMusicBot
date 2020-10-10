using Microsoft.Azure.Cosmos;
using NewMusicBot.Infrastructure.CosmosDb.Command;
using NewMusicBot.Infrastructure.CosmosDb.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewMusicBot.Infrastructure.CosmosDb
{
    public interface ICosmosDataLayer
    {
        T ExecuteQuery<T>(ICosmosQuery<T> query);
        T ExecuteCommand<T>(ICosmosCommand<T> query);
    }

    public class CosmosDataLayer : ICosmosDataLayer
    {
        private readonly CosmosClient client;

        public CosmosDataLayer(CosmosClient client) => 
            this.client = client;

        public T ExecuteQuery<T>(ICosmosQuery<T> query) => query.Execute(client);
        public T ExecuteCommand<T>(ICosmosCommand<T> query) => query.Execute(client);
    }

    public static class CosmosDataLayerValues
    {
        public const string DatabaseName = "NewMusicBot";
        public const string ChannelDataContainerName = "DiscordChannel";
    }
}
