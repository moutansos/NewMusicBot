using AsciiTableFormatter;
using Discord.Commands;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("list")]
    public class ListModule : ModuleBase<SocketCommandContext>
    {
        private readonly INewMusicBotService service;

        public ListModule(INewMusicBotService service) =>
            this.service = service;

        [Command]
        public async Task ListCommand() => await ReplyAsync("List things");

        [Command("artists")]
        public async Task ListArtists()
        {
            ulong channelId = Context.Channel.Id;
            IEnumerable<SubscribedArtist> artists = await service.GetAllSubscribedArtists(channelId);

            if(artists.Count() == 0)
            {
                await ReplyAsync("No artists are subscribed in this channel!");
                return;
            }

            var allArtists = artists
                .OrderBy(artist => artist.Name)
                .Select(artist => new { Name = artist.Name, Id = artist.Id });

            string formattedArtists = Formatter.Format(allArtists);

            string message = $"All currently subscribed artists: \n```\n{formattedArtists}```";
            await ReplyAsync(message);
        }
    }
}
