using Discord.Commands;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("remove")]
    public class RemoveModule : ModuleBase<SocketCommandContext>
    {
        private readonly INewMusicBotService service;

        public RemoveModule(INewMusicBotService service) =>
            this.service = service;

        [Command]
        public async Task RemoveCommand() => await ReplyAsync("Remove things");

        [Command("artist")]
        public async Task RemoveArtist([Remainder] string id)
        {
            ulong channelId = Context.Channel.Id;
            SubscribedArtist? artist = await service.RemoveSubscribedArtist(channelId, artistId: id);
            
            if(artist is null)
            {
                await ReplyAsync($"No artist with that id was found.");
                return;
            }

            await ReplyAsync($"Sucessfully removed artist: {artist.Name}");
        }
    }
}
