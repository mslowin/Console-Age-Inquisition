using ConsoleAgeInquisition.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ConsoleAgeInquisition.Services;

public static class FilesService
{
    /// <summary>
    /// Saves the game in as a json file.
    /// </summary>
    /// <param name="game">Game objeect to be saved.</param>
    /// <param name="saveName">Name of the save, e.g. "save1".</param>
    /// <returns>True if the game has been sucessfully saved, otherwise false.</returns>
    public static bool SaveGame(Game game, string saveName)
    {
        var gameSavesFolderPath = Resources.GetGameSavesFolderPath();

        // Kiedyś może przenieść savy do AppData folder
        ////string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        ////string gameSavesPath = Path.Combine(appDataFolder, "ConsoleAgeInquisition", "GameSaves");

        if (!Directory.Exists(gameSavesFolderPath))
        {
            Directory.CreateDirectory(gameSavesFolderPath);
        }

        var saveFilePath = Path.Combine(gameSavesFolderPath, $"{saveName}.json");

        if (File.Exists(saveFilePath))
        {
            Console.WriteLine("A save with this name already exists.");
            // TODO: Do you want to overwrite it?

            return false;
        }
        else
        {
            Console.WriteLine("Saving the game...");
            try
            {
                string jsonData = JsonConvert.SerializeObject(game, Formatting.Indented);
                File.WriteAllText(saveFilePath, jsonData);
                Console.WriteLine("Game saved successfully");

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't save the game, try again.");
                return false;
            }
        }
    }

    public static Game? LoadGame()
    {
        var saveName = ViewsService.HandleLoadGameMenu();

        if (saveName == null)
        {
            return null;
        }

        var jsonData = File.ReadAllText(saveName);

        return JsonConvert.DeserializeObject<Game>(jsonData);
    }

    ////public bool CheckIfSaveFileAlreadyExists(string saveFilePath)
    ////{
    ////    if (File.Exists(saveFilePath))
    ////    {
    ////        return true;
    ////    }

    ////    return false;
    ////}
}
