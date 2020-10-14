using Discord.Commands;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("select")]
    public class SelectModule : ModuleBase<SocketCommandContext>
    {
        private readonly INewMusicBotService service;

        public SelectModule(INewMusicBotService service) => this.service = service;

        [Command("artist")]
        public async Task SelectArtist(int selection)
        {
            ulong channelId = Context.Channel.Id;
            SubscribedArtist? artist = await service.SelectArtistToSubscribeTo(channelId, selection);

            if(artist is null)
            {
                await ReplyAsync("Unable to select an artist with that number. Please run !add artist <<my search params>> to search for an artist.");
                return;
            }

            await ReplyAsync($"Successfully subscribed to {artist.Name}. Currently tracking {artist.Albums.Count()} releases.");
        }
    }
}
