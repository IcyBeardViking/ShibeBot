using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShibeBot
{
    class Program
    {
        readonly string botToken = "NDg1NTgxMzY2NDY1OTIxMDM0.DmzG3w.0CHYLZByl2RFm7uaXv0PUtfYFW4";

        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;



        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            await client.LoginAsync(TokenType.Bot, botToken);

            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommandAsync;
            // Discover all of the commands in this assembly and load them.
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            await Log(new LogMessage(LogSeverity.Info, "Message:", arg.Content));

            //if null return, otherwise put <arg> into <msg>
            if (!(arg is SocketUserMessage msg)) return;
            if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;

            int pos = 0;
            if (msg.HasMentionPrefix(client.CurrentUser, ref pos))
            {
              await arg.Channel.SendMessageAsync("Bork! Bork!");
            }
            return;
        }

    }
}
