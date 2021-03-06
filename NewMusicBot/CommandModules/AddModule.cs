﻿using Discord.Commands;
using Discord.WebSocket;
using NewMusicBot.Models;
using NewMusicBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("add")]
    public class AddModule : ModuleBase<SocketCommandContext>
    {
        private readonly INewMusicBotService service;

        public AddModule(INewMusicBotService service) =>
            this.service = service;

        [Command]
        public async Task AddCommand() => await ReplyAsync("Add things");

        [Command("artist")]
        public async Task AddArtist([Remainder] string artistSearchQuery)
        {
            ulong channelId = Context.Channel.Id;
            SocketTextChannel channel = Context.Channel as SocketTextChannel ?? throw new InvalidOperationException("Channel was not text channel");
            IEnumerable<Artist> artists = await service.InitiateArtistSubscriptionSearch(channelId, channel.Guild.Id, artistSearchQuery);

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
