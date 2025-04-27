namespace Bsports;

public class BotLoader<TBot>
    where TBot : IBot
{
    public TBot LoadBot(string path)
    {
        // Load the assembly
        var context = new BotLoadContext(path);
        var assembly = context.LoadFromAssemblyPath(path);

        // Find the type of the bot
        var botType = assembly.GetTypes()
            .FirstOrDefault(t => typeof(TBot).IsAssignableFrom(t) && !t.IsAbstract);

        if (botType == null)
        {
            throw new InvalidOperationException($"No valid bot found in {path}");
        }

        // Create an instance of the bot
        return (TBot)Activator.CreateInstance(botType);
    }

    public IList<TBot> LoadBots(params string[] paths)
    {
        // Load each bot and return a list of them
        var bots = new List<TBot>();
        foreach (var path in paths)
        {
            try
            {
                var bot = LoadBot(path);
                bots.Add(bot);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load bot from {path}: {ex.Message}");
            }
        }

        return bots;
    }

    public IList<TBot> LoadBots(string directory)
    {
        return LoadBots(Directory.GetFiles(directory, "*.dll"));
    }
}