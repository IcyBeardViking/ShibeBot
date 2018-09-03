using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ShibeBot.Modules;
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

        // Add bot with this link:
        // https://discordapp.com/oauth2/authorize?client_id=485581366465921034&scope=bot

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
                .AddSingleton<AudioService>()
                .BuildServiceProvider();

            await InstallCommandsAsync();

            client.Log += Log;
            //client.MessageReceived += MessageReceived;

            await client.LoginAsync(TokenType.Bot, botToken);

            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        public static Task Log(LogMessage msg)
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

        //private async Task MessageReceived(SocketMessage arg)
        //{
        //    await Log(new LogMessage(LogSeverity.Info, "Message:", arg.Content));
        //
        //    //if null return, otherwise put <arg> into <msg>
        //    if (!(arg is SocketUserMessage msg)) return;
        //    if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;
        //
        //    int pos = 0;
        //    if (msg.HasMentionPrefix(client.CurrentUser, ref pos))
        //    {
        //      await arg.Channel.SendMessageAsync("Bork! Bork!");
        //    }
        //    return;
        //}

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            if (!(messageParam is SocketUserMessage message)) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command,
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new SocketCommandContext(client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync("*Twists head*");
                await context.Channel.SendMessageAsync("*Confused-Bork!*  (" + result.ErrorReason + ")");
            }
        }

    }
}
