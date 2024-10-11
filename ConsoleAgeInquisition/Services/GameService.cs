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
            Console.Write("\n> ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                commandService.ExecuteCommand(input);
            }
        }

        // TODO: do przetestowania podnoszenie różnych typów itemków za pomocą komendy pickup

        // TODO: po pokonaniu bossa koniec gry i jakiś tekst podsumowujący (A właściwie gra powinna się
        // TODO: kończyć w momencie kiedy w ekwipunku Hero znajdzie się np. diament, który wypada z bosa)
        // TODO: czyli co iteracja jest jakis if (HeroFoundDiamond)

        // TODO: ważne, nie można otworzyć skrzyni, jeśli w pokoju są przeciwnicy
        // TODO: (komenda open, która wysypuje zawartość skrzyni na ziemie i usuwa skrzynie z pokoju)

        // TODO: nazwy objektów nigdy nie mogą się powtarzać wewnątrz jednego pokoju

        // TODO: komenda w stylu USE -> np używasz jedzenia z ekwipunku, aby się uleczyć, albo potki
    }

    private static void Initialize(CommandService commandService, Game game)
    {
        commandService.RegisterCommand("attack", new AttackCommand(game.Dungeon), "Attack a specified target. Usage: attack [target]");
        commandService.RegisterCommand("go", new GoCommand(game.Dungeon), "Go in selected direction. Usage: go [direction] (north, south, east, west)");
        commandService.RegisterCommand("pickup", new PickUpCommand(game.Dungeon), "Pick up an item. Usage: pickup [item]");
        commandService.RegisterCommand("save", new SaveCommand(game), "Save current state of the game.");
        commandService.RegisterCommand("look", new LookCommand(game.Dungeon), "Get information about the current surroundings.");
        commandService.RegisterCommand("examine", new ExamineCommand(game.Dungeon), "Get more information about an object or enemy. Usage: examine [object]");
        commandService.RegisterCommand("help", new HelpCommand(commandService), "List all available commands.");
        commandService.RegisterCommand("restart", new RestartCommand(game), "Restart the game with optional saving.");
        commandService.RegisterCommand("exit", new ExitCommand(game), "Exit the game with optional saving.");

        // Potem reszta komend
    }
}