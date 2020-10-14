using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.SpotifyApi.Queries
{
    public class AlbumsForArtistQuery : ISpotifyQuery<IAsyncEnumerable<SimpleAlbum>>
    {
        private readonly string id;

        public AlbumsForArtistQuery(string id) => this.id = id;

        public async IAsyncEnumerable<SimpleAlbum> ExecuteAsync(ISpotifyClient client)
        {
            Paging<SimpleAlbum>? albums = await client.Artists.GetAlbums(id);

            await foreach (SimpleAlbum album in client.Paginate(albums))
                yield return album;
        }
    }
}
