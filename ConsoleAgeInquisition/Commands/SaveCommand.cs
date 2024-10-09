using ConsoleAgeInquisition.Models;
using ConsoleAgeInquisition.Services;
using Newtonsoft.Json;

namespace ConsoleAgeInquisition.Commands;

public class SaveCommand : ICommand
{
    private readonly Game _game;

    public SaveCommand(Game game)
    {
        _game = game;
    }

    public void Execute(string[] args)
    {
        // TODO: The arg needs to be a name of the save (name needs to be valid - only letters and numbers - no number as first letter)

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a valid save name.");
            return;
        }

        var saveName = args[0];

        _ = FilesService.SaveGame(_game, saveName);
    }
}