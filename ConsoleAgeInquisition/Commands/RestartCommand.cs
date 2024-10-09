using ConsoleAgeInquisition.Models;
using ConsoleAgeInquisition.Services;
using System.Diagnostics;

namespace ConsoleAgeInquisition.Commands;

public class RestartCommand : ICommand
{
    private readonly Game _game;

    public RestartCommand(Game game)
    {
        _game = game;
    }

    public void Execute(string[] args)
    {
        var choice = ViewsService.HandleSaveAndRestartMenu();

        if (choice == 1)
        {
            // Save and restart
            bool isSavingSucessfull = false;
            while (!isSavingSucessfull)
            {
                Console.WriteLine("Type a name for the save.");
                var saveName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(saveName))
                {
                    Console.WriteLine("Invalid save name. Please provide a valid name.");
                }
                else
                {
                    isSavingSucessfull = FilesService.SaveGame(_game, saveName);
                }
            }

            RestartApp();
        }

        if (choice == 2)
        {
            RestartApp();
        }
    }

    private static void RestartApp()
    {
        Console.WriteLine("Restarting the game...");

        var exePath = Environment.ProcessPath;
        if (exePath == null)
        {
            Console.WriteLine("Something went wrong. Game couldn't be restarted.");
            return;
        }

        // Starting new app instance
        Process.Start(exePath);

        // Killing the current one
        Environment.Exit(0);
    }
}