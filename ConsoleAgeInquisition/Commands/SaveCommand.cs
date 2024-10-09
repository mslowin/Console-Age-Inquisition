using ConsoleAgeInquisition.Models;
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

        var gameSavesFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../GameSaves"));

        // Kiedyś może przenieść savy do AppData folder
        ////string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        ////string gameSavesPath = Path.Combine(appDataFolder, "ConsoleAgeInquisition", "GameSaves");

        if (!Directory.Exists(gameSavesFolderPath))
        {
            Directory.CreateDirectory(gameSavesFolderPath);
        }

        var saveName = args[0];
        var saveFilePath = Path.Combine(gameSavesFolderPath, $"{saveName}.json");

        if (File.Exists(saveFilePath))
        {
            Console.WriteLine("A save with this name already exists.");
            // TODO: Do you want to overwrite it?
        }
        else
        {
            Console.WriteLine("Saving the game...");
            try
            {
                string jsonData = JsonConvert.SerializeObject(_game, Formatting.Indented);
                File.WriteAllText(saveFilePath, jsonData);
                Console.WriteLine("Game saved successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't save the game, try again.");
            }
        }
    }
}