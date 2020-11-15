using Discord.Commands;
using Discord.WebSocket;
using NewMusicBot.Services;
using System;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("ignore")]
    public class IgnoreModule : ModuleBase<SocketCommandContext>
    {
        private readonly INewMusicBotService service;

        public IgnoreModule(INewMusicBotService service) => 
            this.service = service;

        [Command]
        public async Task IgnoreCommand()
        {
            ulong channelId = Context.Channel.Id;
            SocketTextChannel channel = Context.Channel as SocketTextChannel ?? throw new InvalidOperationException("Channel was not text channel");
            await service.IgnoreChannel(channelId, channel.Guild.Id);
        }
    }
}
