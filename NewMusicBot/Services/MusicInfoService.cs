using NewMusicBot.Infrastructure.SpotifyApi;
using NewMusicBot.Infrastructure.SpotifyApi.Queries;
using NewMusicBot.Models;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicBot.Services
{
    public interface IMusicInfoService
    {
        Task<SubscribedArtist> RetrieveSubscribedArtist(string id);
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

        public async Task<SubscribedArtist> RetrieveSubscribedArtist(string id)
        {
            IEnumerable<SimpleAlbum> albums = await dataLayer.ExecuteQuery(new AlbumsForArtistQuery(id)).ToListAsync();
            FullArtist artist = await dataLayer.ExecuteQuery(new ArtistByIdQuery(id));

            return new SubscribedArtist(id, artist.Name, albums.Select(album => album.Id));
        }
    }
}
