using Microsoft.Azure.Cosmos;
using NewMusicBot.Models;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.CosmosDb.Command
{
    public class UpdateDiscordChannelCommand: ICosmosCommand<Task<DiscordChannel>>
    {
        private readonly DiscordChannel updatedChannel;

        public UpdateDiscordChannelCommand(DiscordChannel updatedChannel) => this.updatedChannel = updatedChannel;

        public async Task<DiscordChannel> Execute(CosmosClient client)
        {
            Container container = client.GetContainer(CosmosDataLayerValues.DatabaseName, CosmosDataLayerValues.ChannelDataContainerName);
            ItemResponse<DiscordChannel> result = await container.ReplaceItemAsync(updatedChannel, updatedChannel.Id);
            return result.Resource;
        }

    }
}
