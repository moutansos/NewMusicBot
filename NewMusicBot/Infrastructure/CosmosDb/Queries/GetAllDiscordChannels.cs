using Microsoft.Azure.Cosmos;
using NewMusicBot.Models;
using NewMusicBot.Utils;
using System.Collections.Generic;

namespace NewMusicBot.Infrastructure.CosmosDb.Queries
{
    class GetAllDiscordChannels : ICosmosQuery<IAsyncEnumerable<DiscordChannel>>
    {
        public IAsyncEnumerable<DiscordChannel> Execute(CosmosClient client)
        {
            Container container = client.GetContainer(CosmosDataLayerValues.DatabaseName, CosmosDataLayerValues.ChannelDataContainerName);

            QueryDefinition query = new QueryDefinition("SELECT * FROM DiscordChannel");

            FeedIterator<DiscordChannel> feed = container.GetItemQueryIterator<DiscordChannel>(query);
            return feed.AsEnumerableAsync();
        }
    }
}
