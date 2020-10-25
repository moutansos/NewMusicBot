using SpotifyAPI.Web;
using System.Threading.Tasks;

namespace NewMusicBot.Infrastructure.SpotifyApi.Queries
{
    public class AlbumByIdQuery : ISpotifyQuery<Task<FullAlbum>>
    {
        private readonly string albumId;

        public AlbumByIdQuery(string albumId) => this.albumId = albumId;

        public async Task<FullAlbum> ExecuteAsync(ISpotifyClient client) => 
            await client.Albums.Get(albumId);
    }
}
