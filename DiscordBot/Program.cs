using System.Xml;
using System.Xml.Linq;
using Discord;
using Discord.WebSocket;
using DiscordBot;

public class Program
{
    public static Task Main(string[] args) => new Bot().MainAsync();
}