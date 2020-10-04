using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    public class PingModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Replys with the message")]
        public async Task PingAsync([Summary("The thing to say back")] string message) =>
            await ReplyAsync($"Pong! {message}");
    }
}
