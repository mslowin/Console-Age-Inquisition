using ConsoleAgeInquisition.Models;
using ConsoleAgeInquisition.Services;

namespace ConsoleAgeInquisition.Commands;

public class ExitCommand : ICommand
{
    private readonly Game _game;

    public ExitCommand(Game game)
    {
        _game = game;
    }

    public void Execute(string[] args)
    {
        var choice = ViewsService.HandleSaveAndExitMenu();

        if (choice == 1)
        {
            // Save and exit
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

            Console.WriteLine("Exiting the game...");
            Environment.Exit(0);
        }

        if (choice == 2)
        {
            // Just exit
            Console.WriteLine("Exiting the game...");
            Environment.Exit(0);
        }
    }
}
