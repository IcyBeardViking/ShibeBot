using Discord;
using Discord.Audio;
using Discord.Commands;
using ShibeBot.Data;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{
    public class AudioModule : ModuleBase<SocketCommandContext>
    {
        public AudioService AudioService { get; set; }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel"); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            AudioService.audioClient = await channel.ConnectAsync();
            return;
        }

        [Command("FBI", RunMode = RunMode.Async)]
        public async Task FBI()
        {
            await play(Files.Audio.getCommand(Files.Audio.FBI));
        }

        [Command("USSR", RunMode = RunMode.Async)]
        public async Task USSR()
        {
            await play(Files.Audio.getCommand(Files.Audio.USSR));
        }

        [Command("arf", RunMode = RunMode.Async)]
        public async Task arf()
        {
            await play(Files.Audio.getCommand(Files.Audio.arf));
        }

        [Command("oof", RunMode = RunMode.Async)]
        public async Task oof()
        {
            await play(Files.Audio.getCommand(Files.Audio.oof));
        }

        [Command("bork", RunMode = RunMode.Async)]
        public async Task bork()
        {
            await play(Files.Audio.getCommand(Files.Audio.bork));
        }

        [Command("bork2", RunMode = RunMode.Async)]
        public async Task bork2()
        {
            await play(Files.Audio.getCommand(Files.Audio.bork2));
        }

        [Command("newtoy", RunMode = RunMode.Async)]
        public async Task newtoy()
        {
            await play(Files.Audio.getCommand(Files.Audio.newtoy));
        }

        [Command("cutelaugh", RunMode = RunMode.Async)]
        public async Task cutelaugh()
        {
            await play(Files.Audio.getCommand(Files.Audio.cutelaugh));
        }

        [Command("nani", RunMode = RunMode.Async)]
        public async Task nani()
        {
            await play(Files.Audio.getCommand(Files.Audio.nani));
        }

        [Command("omaenani", RunMode = RunMode.Async)]
        public async Task omaenani()
        {
            await play(Files.Audio.getCommand(Files.Audio.omaenani));
        }

        [Command("omae", RunMode = RunMode.Async)]
        public async Task omae()
        {
            await play(Files.Audio.getCommand(Files.Audio.omae));
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task play(string input = null)
        {

            IVoiceChannel channel = (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel"); return; }
            //TODO add if bot is already in channel

            // Checking if the bot is already in the channel
            if ((Context.Client.CurrentUser as IGuildUser)?.VoiceChannel == null)
            {
                // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
                AudioService.audioClient = await channel.ConnectAsync();
            }
            if (AudioService.audioClient is null) { await Context.Message.Channel.SendMessageAsync("Bork! Bork! Can't play without joining!"); return; }

            if (input != null)
            {
                input = getInput(input);
            }

            await SendAsync(AudioService.audioClient, input);
            return;
        }

        [Command("stop")]
        public async Task stop()
        {
            if (!AudioService.audioProcess.HasExited)
                AudioService.audioProcess.Kill();
            else
                await ReplyAsync("*Silence*");
        }

        [Command("kick")]
        public async Task DisconnectFromChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Client.CurrentUser as IGuildUser)?.VoiceChannel;

            // if process has not exited yet, kill it
            if (!AudioService.audioProcess.HasExited)
                AudioService.audioProcess.Kill();

            await AudioService.audioClient.StopAsync();
            return;
        }


        //#########################################################################################################################
        //#########################################################################################################################
        //#########################################################################################################################

        private string getInput(string input)
        {
            input = input.Trim().ToLower();

            switch (input)
            {
                case "despacito":
                    input = Files.Audio.getCommand(Files.Audio.despasito);
                    break;
                case "despacito2":
                    input = Files.Audio.getCommand(Files.Audio.despasito2);
                    break;
                case "despacito3":
                    input = Files.Audio.getCommand(Files.Audio.despasito3);
                    break;
            }

            return input;
        }

        private Process CreateStream(string input)
        {
            ProcessStartInfo ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = input,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process streamingProcess = new Process();

            streamingProcess.StartInfo = ffmpeg;
            streamingProcess.Exited += disconnectAfterAudioEffect;
            streamingProcess.EnableRaisingEvents = true;


            streamingProcess.Start();

            return streamingProcess;
        }

        public void disconnectAfterAudioEffect(object sender, EventArgs e)
        {
            DisconnectFromChannel();
        }


        private async Task SendAsync(IAudioClient client, string input)
        {


            using (AudioService.audioProcess = CreateStream(input))
            using (var output = AudioService.audioProcess.StandardOutput.BaseStream)
            using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }
    }
}

