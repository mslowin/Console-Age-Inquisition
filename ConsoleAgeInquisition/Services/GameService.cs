﻿using ConsoleAgeInquisition.Commands;
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
        Initialize(commandService, game.Dungeon);

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

        // TODO: ZAPISYWANIE GRY!
        // TODO: komenda do zapisania gry

        // TODO: komenda do wyjścia z gry

        // TODO: komenda examine, żeby np uzyskać informacji więcej o przeciwniku

        // TODO: ważne, nie można przejść do następenego pokoju, jeśli w pokoju są przeciwnicy, można jedynie wrócić do poprzedniego

        // TODO: ważne, nie można otworzyć skrzyni, jeśli w pokoju są przeciwnicy
    }

    private static void Initialize(CommandService commandService, Dungeon dungeon)
    {
        commandService.RegisterCommand("attack", new AttackCommand(dungeon), "Attack a specified target. Usage: attack [target]");
        commandService.RegisterCommand("pickup", new PickUpCommand(), "Pick up an item. Usage: pickup [item]");
        commandService.RegisterCommand("look", new LookCommand(dungeon), "Get information about the current surroundings.");
        commandService.RegisterCommand("help", new HelpCommand(commandService), "List all available commands.");

        // Potem reszta komend
    }
}