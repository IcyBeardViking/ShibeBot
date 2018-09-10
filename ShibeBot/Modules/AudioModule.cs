using Discord;
using Discord.Audio;
using Discord.Commands;
using ShibeBot.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{
    public class AudioModule : ModuleBase<SocketCommandContext>
    {
        public AudioService AudioService { get; set; }

        private CommandInfo info { get; set; }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel"); return; }

            AudioService.leaveAfterExecution = false;

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            AudioService.audioClient = await channel.ConnectAsync();
            return;
        }

        [Command("FBI", RunMode = RunMode.Async)]
        public async Task FBI()
        {
            await play(info.Name);
        }

        [Command("USSR", RunMode = RunMode.Async)]
        public async Task USSR()
        {
            await play(info.Name);
        }

        [Command("arf", RunMode = RunMode.Async)]
        public async Task arf()
        {
            await play(info.Name);
        }

        [Command("oof", RunMode = RunMode.Async)]
        public async Task oof()
        {
            await play(info.Name);
        }

        [Command("bork", RunMode = RunMode.Async)]
        public async Task bork()
        {
            await play(info.Name);
        }

        [Command("bork2", RunMode = RunMode.Async)]
        public async Task bork2()
        {
            await play(info.Name);
        }

        [Command("newtoy", RunMode = RunMode.Async)]
        public async Task newtoy()
        {
            await play(info.Name);
        }

        [Command("cutelaugh", RunMode = RunMode.Async)]
        public async Task cutelaugh()
        {
            await play(info.Name);
        }

        [Command("nani", RunMode = RunMode.Async)]
        public async Task nani()
        {
            await play(info.Name);
        }

        [Command("omaenani", RunMode = RunMode.Async)]
        public async Task omaenani()
        {
            await play(info.Name);
        }

        [Command("omae", RunMode = RunMode.Async)]
        public async Task omae()
        {
            await play(info.Name);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task play(string input = null)
        {
            IVoiceChannel channel = (Context.Message.Author as IGuildUser)?.VoiceChannel;
            // Checking if the current user is in a channel or not
            if (channel == null)
            {
                await Context.Message.Channel.SendMessageAsync("User must be in a voice channel");
                return;
            }
            
            if (input != null)
            {
                try
                {
                    input = Files.Audio.getCommand(input);
                }
                catch (FileNotFoundException e)
                {
                    await Context.Channel.SendMessageAsync("*Twists head*");
                    await Context.Channel.SendMessageAsync("*Confused-Bork!*  (" + e.Message + ")");
                    return;
                }
            }

            // Checking if the bot is already in the channel
            if (AudioService.audioClient == null)
            {
                AudioService.audioClient = await channel.ConnectAsync();
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

            AudioService.leaveAfterExecution = true;

            // if process is not null and has not exited yet, kill it
            if ((AudioService.audioProcess != null) && !AudioService.audioProcess.HasExited)
                AudioService.audioProcess.Kill();

            if(AudioService.audioClient != null)
                await AudioService.audioClient.StopAsync();

            AudioService.audioClient = null;
            return;
        }


        //#########################################################################################################################
        //#########################################################################################################################
        //#########################################################################################################################

        protected override void BeforeExecute(CommandInfo command)
        {
            info = command;
            base.BeforeExecute(command);
        }

        private Process CreateStream(string input)
        {
            ProcessStartInfo ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = input,
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };

            Process streamingProcess = new Process();

            streamingProcess.StartInfo = ffmpeg;
            if (AudioService.leaveAfterExecution)
            { 
                streamingProcess.Exited += disconnectAfterAudioEffect;
            }
            streamingProcess.Exited += cleanVoiceServiceAfterEffect;
            streamingProcess.EnableRaisingEvents = true;

            streamingProcess.Start();

            return streamingProcess;
        }

        public void disconnectAfterAudioEffect(object sender, EventArgs e)
        {
            DisconnectFromChannel();
        }

        public void cleanVoiceServiceAfterEffect(object sender, EventArgs e)
        {
            AudioService.audioProcess = null;
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

