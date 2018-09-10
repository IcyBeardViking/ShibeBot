using Discord.Commands;
using Discord.WebSocket;
using ShibeBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{
    public class PictureModule : ModuleBase<SocketCommandContext>
    {
        [Command("sleepy")]
        public async Task sleepy(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.SLEEPY));
            return;
        }

        [Command("excited")]
        public async Task excited(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.EXCITED));
            return;
        }

        [Command("angery")]
        public async Task angery(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.ANGERY));
            return;
        }

        [Command("happy")]
        public async Task happy(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.HAPPY));
            return;
        }

        [Command("heck")]
        public async Task heck(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.HECK));
            return;
        }

        [Command("meme")]
        public async Task meme(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.MEME));
            return;
        }

        [Command("wow")]
        public async Task wow(int picNumber = -1)
        {

            await shibePic(Files.Pictures.getShibePath(picNumber, ShibeType.WOW));
            return;
        }

        [Command("shibe")]
        public async Task shibePic(string path)
        {
            await Context.Channel.SendFileAsync(path);
            return;
        }
    }
}
