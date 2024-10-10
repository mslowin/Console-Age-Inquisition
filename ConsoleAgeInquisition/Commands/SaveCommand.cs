using ConsoleAgeInquisition.Models;
using ConsoleAgeInquisition.Services;

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
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a valid save name.");
            return;
        }

        var saveName = args[0];

        _ = FilesService.SaveGame(_game, saveName);
    }
}