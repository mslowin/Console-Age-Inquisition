using ConsoleAgeInquisition.Commands;
using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Services;

/// <summary>
/// Service responsible for running the actual game.
/// </summary>
public static class GameService
{
    public static void Run(Game game)
    {
        var commandService = new CommandService();
        Initialize(commandService, game);

        ViewsService.IntroducePlayer();

        while (true)
        {
            ////if (!game.Dungeon.Rooms.Exists(room => room.Enemies.Count > 0))
            ////{
            ////    // When the are no more enemies, the game ends
            ////    break;
            ////}

            var hero = game.Dungeon.Rooms.Find(room => room.Hero != null)!.Hero;
            var diamondOre = Resources.GetDiamondOre();
            if (hero != null
                && hero.Items != null
                && hero.Items.Exists(item => item.Name == diamondOre.Name && item.Type == diamondOre.Type))
            {
                // When the hero picks up diamonds, the game ends
                ViewsService.VictoryMessage();
                break;
            }

            if (hero != null && hero.Health <= 0)
            {
                // When the hero dies
                ViewsService.YouDiedMessage();
                break;
            }

            Console.Write("\n> ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                commandService.ExecuteCommand(input);
            }
        }

        // TODO: do przetestowania podnoszenie różnych typów itemków za pomocą komendy pickup
    }

    public static void Initialize(CommandService commandService, Game game)
    {
        commandService.RegisterCommand("look", new LookCommand(game.Dungeon), "Get information about the current surroundings.");
        commandService.RegisterCommand("stats", new StatsCommand(game.Dungeon), "Get information about the Hero.");
        commandService.RegisterCommand("attack", new AttackCommand(game.Dungeon), "Attack a specified target. Usage: attack [target]");
        commandService.RegisterCommand("use", new UseCommand(game.Dungeon), "Use an object. Usage: use [object] (food, potions)");
        commandService.RegisterCommand("go", new GoCommand(game.Dungeon), "Go in selected direction. Usage: go [direction] (north, south, east, west)");
        commandService.RegisterCommand("open", new OpenCommand(game.Dungeon), "Open selected chest. Usage: open [chest]");
        commandService.RegisterCommand("pickup", new PickUpCommand(game.Dungeon), "Pick up an item. Usage: pickup [item]");
        commandService.RegisterCommand("examine", new ExamineCommand(game.Dungeon), "Get more information about an object or enemy. Usage: examine [object]");
        commandService.RegisterCommand("save", new SaveCommand(game), "Save current state of the game.");
        commandService.RegisterCommand("restart", new RestartCommand(game), "Restart the game with optional saving.");
        commandService.RegisterCommand("exit", new ExitCommand(game), "Exit the game with optional saving.");
        commandService.RegisterCommand("help", new HelpCommand(commandService), "List all available commands.");

        // Potem reszta komend
    }
}