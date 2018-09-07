using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{

    public class SpeechModule : ModuleBase<SocketCommandContext>
    {

        [Command("say")]
        [Summary("Echoes a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            string reply = "";
            string bork = "Bork! ";

            for (int i = 0; i < echo.Trim().Length - 1; i++)
            {
                if (echo[i] == ' ' && echo[i + 1] != ' ')
                    reply += bork;
            }
            reply += bork;

            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(reply);
            return;
        }

        [Command("talkto")]
        public async Task TalkTo(SocketUser user = null)
        {
            string reply = "";

            if (user != null)
                reply += $"Bork! Bork! " + user.Mention + "Bork!";
            else
                reply += "Bork?!";

            await ReplyAsync(reply);
            return;
        }

        [Command("pet")]
        public async Task Pet(SocketUser user = null)
        {
            string reply = "*Excited*";


            await ReplyAsync(reply);

            if (user != null)
            {
                reply = "*Boops* " + user.Mention + "*'s nose*";
            }
            else
                reply = "BORK!";

            await ReplyAsync(reply);
            return;
        }

        [Command("pat")]
        public async Task Pat(SocketUser user = null)
        {
            string reply = "";

            if (user != null)
            {
                reply = "*Hands paw to* " + user.Mention;
            }
            else
            {
                reply = "*Head-twist*";
                await ReplyAsync(reply);
                reply = "B-b-bork?!";
            }

            await ReplyAsync(reply);
            return;
        }

        [Command("help")]
        public async Task help()
        {
            string reply = "Commands I know are: \n-help\n-pat <someone-optional>\n-pet <someone-optional>\n-talkto <someone>\n-say <something>\n-arf\n-oof\n-pat\n-bork\n-bork2\n-play <something>\n-stop\n-kick\n-join\n-newtoy\n-cutelaugh\n-nani\n-omae\n-omaenani";

            await ReplyAsync(reply);
            return;
        }



    }
}