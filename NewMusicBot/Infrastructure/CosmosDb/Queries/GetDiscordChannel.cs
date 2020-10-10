using Microsoft.Azure.Cosmos;
using NewMusicBot.Models;
using NewMusicBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.CosmosDb.Queries
{
    class GetDiscordChannel : ICosmosQuery<Task<DiscordChannel?>>
    {
        private readonly ulong channelId;

        public GetDiscordChannel(ulong channelId) => 
            this.channelId = channelId;

        public async Task<DiscordChannel?> Execute(CosmosClient client)
        {
            Container container = client.GetContainer(CosmosDataLayerValues.DatabaseName, CosmosDataLayerValues.ChannelDataContainerName);

            QueryDefinition query = new QueryDefinition("SELECT * FROM DiscordChannel WHERE DiscordChannel.id = @id")
		        .WithParameter("@id", channelId.ToString());

            FeedIterator<DiscordChannel> feed = container.GetItemQueryIterator<DiscordChannel>(query);
            IAsyncEnumerable<DiscordChannel> discordChannels = feed.AsEnumerableAsync();
            return await discordChannels.FirstOrDefaultAsync();
        }
    }
}
