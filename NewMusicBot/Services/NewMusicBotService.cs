﻿using NewMusicBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Services
{
    public interface INewMusicBotService
    {
        Task<IEnumerable<Artist>> InitiateArtistSubscriptionSearch(ulong channelId, string artistQuery);
        Task<SubscribedArtist?> SelectArtistToSubscribeTo(ulong channelId, int selection);
    }

    public class NewMusicBotService : INewMusicBotService
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IMusicInfoService musicInfoService;

        public NewMusicBotService(ISubscriptionService subscriptionService, IMusicInfoService musicInfoService)
        {
            this.subscriptionService = subscriptionService;
            this.musicInfoService = musicInfoService;
        }


        public async Task<IEnumerable<Artist>> InitiateArtistSubscriptionSearch(ulong channelId, string artistQuery)
        {
            IEnumerable<Artist> artists = await musicInfoService.SearchForArtist(artistQuery)
                .ToListAsync();

            IEnumerable<(int, Artist)> artistsWithIndex = artists.Select((artist, index) => (index + 1, artist));

            DiscordChannel? channel = await subscriptionService.GetChannel(channelId);

            Dictionary<int, string> artistOptions = artistsWithIndex.ToDictionary(
                keySelector: artist => artist.Item1,
                elementSelector: artist => artist.Item2.Id);

            if(channel is null)
            {
                DiscordChannel newDiscordChannel = new DiscordChannel(
                    id: $"{channelId}",
                    currentArtistOptions: artistOptions,
                    subscribedArtists: new SubscribedArtist[] { });

                await subscriptionService.CreateChannel(newDiscordChannel);
            }
            else
            {
                DiscordChannel updatedDiscordChannel = channel.WithNewCurrentArtistOptions(artistOptions);

                await subscriptionService.UpdateChannel(updatedDiscordChannel);
            }

            return artists;
        }

        public async Task<SubscribedArtist?> SelectArtistToSubscribeTo(ulong channelId, int selection)
        {
            DiscordChannel? channel = await subscriptionService.GetChannel(channelId);

            string? artistId = channel?.CurrentArtistOptions?[selection];

            if (artistId is null)
                return null;

            SubscribedArtist newArtist = await musicInfoService.RetrieveSubscribedArtist(artistId);
            
            if (channel is null)
                return null;

            DiscordChannel updatedChannel = channel.WithSubscribedArtistAdded(newArtist);
            DiscordChannel channelWithoutCurrent = updatedChannel.WithNewCurrentArtistOptions(new Dictionary<int, string>());

            await subscriptionService.UpdateChannel(channelWithoutCurrent);

            return newArtist;
        }
    }
}
