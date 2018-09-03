using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{

    public class SpeechModule : ModuleBase<SocketCommandContext>
    {
        public AudioService AudioService { get; set; }

        [Command("say")]
        [Summary("Echoes a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            string reply = "";
            string bork = "Bork! ";

            for (int i =0; i < echo.Trim().Length - 1; i++)
            {
                if (echo[i] == ' ' && echo[i+1] != ' ')
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
        public async Task Pet()
        {
            string reply = "*Excited*";


            await ReplyAsync(reply);

            reply = "BORK!";

            await ReplyAsync(reply);
            return;
        }

        [Command("pat")]
        public async Task Pat()
        {
            string reply = "*Head-twist*";


            await ReplyAsync(reply);

            reply = "B-b-bork?!";

            await ReplyAsync(reply);
            return;
        }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            await ReplyAsync("join command executing!");
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel"); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            AudioService.audioClient = await channel.ConnectAsync();
            await ReplyAsync("joined!");
            return;
        }


        [Command("play", RunMode = RunMode.Async)]
        public async Task play(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel"); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            AudioService.audioClient = await channel.ConnectAsync();
            if (AudioService.audioClient is null) { await Context.Message.Channel.SendMessageAsync("Bork! Bork! Can't play without joining!"); return; }
            await SendAsync(AudioService.audioClient);
            return;
        }

        [Command("kick")]
        public async Task DisconnectFromChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Client.CurrentUser as IGuildUser)?.VoiceChannel;

            AudioService.audioClient = null;
            return;
        }


//#########################################################################################################################
//#########################################################################################################################
//#########################################################################################################################

        private Process CreateStream()
        {
            ProcessStartInfo ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = @"-f dshow -i audio=@device_cm_{33D9A762-90C8-11D0-BD43-00A0C911CE86}\wave_{C75B83D1-78B9-4C41-95FD-709A9A8BDE6B} -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            return Process.Start(ffmpeg);
        }

        private async Task SendAsync(IAudioClient client)
        {
            using (var ffmpeg = CreateStream())
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }
    }
}
