namespace Poker.UI.Utils;

/// <summary>
/// Creates and registers all bots for easy access.
///
/// It maybe more ideal to use a plugin approach, but I'm not familiar and would take longer.
///   https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support 
/// </summary>
public static class BotRegistration
{
    public static RandomBot.RandomBot RandomBot => new();
}