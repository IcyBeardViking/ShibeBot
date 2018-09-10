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
        private string separator = "#_#";

        [Command("sleepy")]
        public async Task sleepy(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.SLEEPY);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("excited")]
        public async Task excited(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.EXCITED);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("angery")]
        public async Task angery(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.ANGERY);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("happy")]
        public async Task happy(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.HAPPY);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("heck")]
        public async Task heck(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.HECK);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("meme")]
        public async Task meme(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.MEME);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("wow")]
        public async Task wow(int picNumber = -1)
        {
            string path = Files.Pictures.getShibePath(ref picNumber, ShibeType.WOW);
            await shibePic(picNumber.ToString() + separator + path);
        }

        [Command("shibe")]
        public async Task shibePic(string path = "")
        {
            // Getting teh picture number from the start of the string
            string[] separators = { separator };
            string pictureNumber = path.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0];

            path = path.Replace(pictureNumber + separator, "");

            await Context.Channel.SendFileAsync(path, pictureNumber);
            return;
        }
    }
}
