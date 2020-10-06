using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace NewMusicBot.Infrastructure.SpotifyApi.Queries
{
    public interface ISpotifyQuery<T>
    {
        T ExecuteAsync(ISpotifyClient client);
    }
}
