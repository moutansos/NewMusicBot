using Microsoft.Azure.Cosmos;
using NewMusicBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.CosmosDb.Command
{
    public class AddDiscordChannelCommand : ICosmosCommand<Task<DiscordChannel>>
    {
        private readonly DiscordChannel channel;

        public AddDiscordChannelCommand(DiscordChannel channel) => this.channel = channel;

        public async Task<DiscordChannel> Execute(CosmosClient client)
        {
            Container container = client.GetContainer(CosmosDataLayerValues.DatabaseName, CosmosDataLayerValues.ChannelDataContainerName);

            ItemResponse<DiscordChannel>? result = await container.CreateItemAsync(channel);

            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return result.Resource;
        }
    }
}
