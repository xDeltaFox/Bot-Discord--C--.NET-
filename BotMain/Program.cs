using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BotMain
{
    class Program
    {
        private static DiscordSocketClient _client;
        private IConfiguration _config;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _config = BuildConfig();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, _config["token"]);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .AddYamlFile("config.yaml")
                .Build();
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
    }
}