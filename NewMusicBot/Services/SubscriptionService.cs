using NewMusicBot.Infrastructure.CosmosDb;
using NewMusicBot.Infrastructure.CosmosDb.Command;
using NewMusicBot.Infrastructure.CosmosDb.Queries;
using NewMusicBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewMusicBot.Services
{
    public interface ISubscriptionService
    {
        Task<DiscordChannel> CreateChannel(DiscordChannel channel);
        IAsyncEnumerable<DiscordChannel> GetAllChannels();
        Task<DiscordChannel?> GetChannel(ulong channelId);
        Task<DiscordChannel> UpdateChannel(DiscordChannel channel);
    }

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ICosmosDataLayer cosmosDataLayer;

        public SubscriptionService(ICosmosDataLayer cosmosDataLayer)
        {
            this.cosmosDataLayer = cosmosDataLayer;
        }

        public async Task<DiscordChannel?> GetChannel(ulong channelId) =>
            await cosmosDataLayer.ExecuteQuery(new GetDiscordChannel(channelId));

        public async Task<DiscordChannel> CreateChannel(DiscordChannel channel) =>
            await cosmosDataLayer.ExecuteCommand(new AddDiscordChannelCommand(channel));

        public async Task<DiscordChannel> UpdateChannel(DiscordChannel channel) =>
            await cosmosDataLayer.ExecuteCommand(new UpdateDiscordChannelCommand(channel));

        public IAsyncEnumerable<DiscordChannel> GetAllChannels() =>
            cosmosDataLayer.ExecuteQuery(new GetAllDiscordChannels());
    }
}
