using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Services;

public static class ViewsService
{
    public static int HandleMainMenu()
    {
        Console.WriteLine("Welcome to Console Age: Inquisition");
        Console.WriteLine("Please let me know what do you want me to do:");
        Console.WriteLine("1. Start a new game");
        Console.WriteLine("2. Load game");
        Console.WriteLine("3. Exit");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out var choice))
            {
                if (choice is 1 or 2 or 3)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
            }

            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    public static int HandleSaveAndExitMenu()
    {
        Console.WriteLine("Do you want to save current game state before exiting?");
        return HandleYesNoMenu();
    }

    public static int HandleSaveAndRestartMenu()
    {
        Console.WriteLine("Do you want to save current game state before restarting?");
        return HandleYesNoMenu();
    }

    private static int HandleYesNoMenu()
    {
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out var choice))
            {
                if (choice is 1 or 2)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please select 1 or 2.");
            }

            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    public static string HandleDifficultyLevelMenu()
    {
        var index = 1;
        Console.WriteLine("Select difficulty level for the game:");
        foreach (var type in Enum.GetValues(typeof(DifficultyLevel)))
        {
            Console.WriteLine($"{index}. {type}");
            index++;
        }

        while (true)
        {
            var difficultyLevel = Console.ReadLine();
            if (int.TryParse(difficultyLevel, out var difficultyLevelInt)
                && difficultyLevelInt >= 1
                && difficultyLevelInt <= Enum.GetValues(typeof(DifficultyLevel)).Length)
            {
                return ((DifficultyLevel)(difficultyLevelInt - 1)).ToString();
            }

            Console.WriteLine("Invalid difficulty level. Please choose 1, 2, or 3.");
        }
    }

    public static string HandleHeroTypeMenu()
    {
        var index = 1;
        Console.WriteLine("Choose character type:");
        foreach (var type in Enum.GetValues(typeof(CharacterType)))
        {
            Console.WriteLine($"{index}. {type}");
            index++;
        }

        while (true)
        {
            var heroType = Console.ReadLine();
            if (int.TryParse(heroType, out var heroTypeInt)
                && heroTypeInt >= 1
                && heroTypeInt <= Enum.GetValues(typeof(CharacterType)).Length)
            {
                return ((CharacterType)(heroTypeInt - 1)).ToString();
            }

            Console.WriteLine("Invalid type. Please provide a valid type.");
        }
    }

    public static string HandleHeroNameSelection()
    {
        Console.WriteLine("Type a name for the hero:");
        var heroName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(heroName))
        {
            Console.WriteLine("Invalid name. Please provide a valid name.");
            heroName = Console.ReadLine();
        }

        return heroName;
    }

    public static string HandleGameNameSelection()
    {
        Console.WriteLine("Type a name for this game:");
        var saveName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(saveName))
        {
            Console.WriteLine("Invalid save name. Please provide a valid name.");
            saveName = Console.ReadLine();
        }

        return saveName;
    }

    public static string? HandleLoadGameMenu()
    {
        var gameSavesFolderPath = Resources.GetGameSavesFolderPath();

        if (!Directory.Exists(gameSavesFolderPath))
        {
            Console.WriteLine("No saved games to be loaded.");
            return null;
        }

        var saveFiles = Directory.GetFiles(gameSavesFolderPath, "*.json");

        if (saveFiles.Length == 0)
        {
            Console.WriteLine("No saved games to be loaded.");
            return null;
        }

        var index = 1;
        Console.WriteLine("Select save to be loaded:");
        foreach (var save in saveFiles)
        {
            Console.WriteLine($"{index}. {Path.GetFileName(save)}");
            index++;
        }

        while (true)
        {
            var selectedSave = Console.ReadLine();
            if (int.TryParse(selectedSave, out var selectedSaveInt)
                && selectedSaveInt >= 1
                && selectedSaveInt <= saveFiles.Length)
            {
                return saveFiles[selectedSaveInt - 1];
            }

            Console.WriteLine("Invalid save. Please provide a valid number.");
        }
    }

    public static void IntroducePlayer()
    {
        Console.Write("\n\n\nWelcome, Brave Explorer!");
        Console.Write("\n\nAs the sun sets behind the towering peaks of the Eldar Mountains, casting long shadows " +
                      "across the land, you find yourself standing before the ominous entrance of an ancient dungeon. " +
                      "The cool evening breeze carries with it whispers of forgotten tales and hidden treasures " +
                      "waiting to be uncovered.");
        Console.Write("\n\nMany have already died inside these tombs, but you, however, are driven by more than " +
                      "just the thrill of discovery. Your heart beats with the hope of impressing the ladies.");
        Console.Write("\n\nPrepare yourself, brave soul! Enter the dungeon, claim your treasures, and win the " +
                      "admiration of the fairest ladies! Your adventure begins now!\n\n");
        Console.Write("Type \"look\" to gather information about your surroundings or \"help\" to list all available commands");
    }
}