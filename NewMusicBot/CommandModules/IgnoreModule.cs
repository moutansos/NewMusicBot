using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewMusicBot.CommandModules
{
    [Group("ignore")]
    public class IgnoreModule : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task IgnoreCommand()
        {

        }
    }
}
