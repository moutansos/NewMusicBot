using NewMusicBot.Infrastructure.SpotifyApi;
using NewMusicBot.Infrastructure.SpotifyApi.Queries;
using NewMusicBot.Models;
using SpotifyAPI.Web;
using System.Collections.Generic;

namespace NewMusicBot.Services
{
    public interface IMusicInfoService
    {
        IAsyncEnumerable<Artist> SearchForArtist(string query);
    }

    public class MusicInfoService : IMusicInfoService
    {
        private readonly ISpotifyDataLayer dataLayer;

        public MusicInfoService(ISpotifyDataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }


        public async IAsyncEnumerable<Artist> SearchForArtist(string query)
        {
            IAsyncEnumerable<FullArtist> artists = dataLayer.ExecuteQuery(new ArtistSearchQuery(query));
            await foreach(FullArtist artist in artists)
            {
                yield return new Artist(
                    id: artist.Id,
                    name: artist.Name,
                    url: artist.ExternalUrls["spotify"]);
            }

            yield break;
        }
    }
}
