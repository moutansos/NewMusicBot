using Discord.Commands;
using Microsoft.Extensions.FileSystemGlobbing;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("add")]
    public class AddModule : ModuleBase<SocketCommandContext>
    {
        private readonly IMusicInfoService musicInfoService;

        public AddModule(IMusicInfoService musicInfoService) =>
            this.musicInfoService = musicInfoService;

        [Command]
        public async Task AddCommand() => await ReplyAsync("Add things");

        [Command("artist")]
        public async Task AddArtist([Remainder] string artistSearchQuery)
        {
            IEnumerable<Artist> artists = await musicInfoService
                .SearchForArtist(artistSearchQuery)
                .ToListAsync();

            IEnumerable<string> artistStrings = artists
                .Select((artist, index) => $"{index + 1}. {artist.Name} {artist.Url}");

            if (artistStrings.Count() == 0)
            {
                await ReplyAsync("No artists found");
                return;
            }

            string replyHeader = "Artists found:";
            await ReplyAsync(replyHeader);

            foreach (string artist in artistStrings)
                await ReplyAsync(artist);
        }
    }
}
