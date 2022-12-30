using Discord;
using Discord.Net;
using Discord.WebSocket;
using System.Xml.Linq;

namespace DiscordBot;

public class Bot
{
    private readonly string _token;
    private readonly ulong _guildId;
    private readonly DiscordSocketClient _client = new();

    public Bot()
    {
        var doc = XDocument.Load("data.xml");
        _token = doc.Root.Element("token").Value;
        _guildId = ulong.Parse(doc.Root.Element("guild_id").Value);
    }

    public async Task MainAsync()
    {
        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;

        await Task.Delay(-1);
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong");
    }


    private async Task ClientReady()
    {
        var guild = _client.GetGuild(_guildId);
        var guildCommand = new SlashCommandBuilder();

        guildCommand.WithName("ping");
        guildCommand.WithDescription("A testing command to try bot functionality");

        try
        {
            await guild.CreateApplicationCommandAsync(guildCommand.Build());
        }
        catch (HttpException e)
        {
            Console.WriteLine(e);
        }
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}