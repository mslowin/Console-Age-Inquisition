using ConsoleAgeInquisition.Enums;
using ConsoleAgeInquisition.Models;
using Newtonsoft.Json;

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
            // TODO: ta metoda zamiast indeksu mogłaby już zwracać w sumie obiekt Game
            var jsonData = File.ReadAllText(save);
            Game? game =  JsonConvert.DeserializeObject<Game>(jsonData);

            if (game != null)
            {
                var hero = game.Dungeon.Rooms.Single(x => x.Hero != null).Hero;
                Console.WriteLine($"{index}. {Path.GetFileName(save)} ({game!.DifficultyLevel})");
                Console.WriteLine($"    - current room: {game.Dungeon.Rooms.Single(x => x.Hero != null).RoomName}");
                Console.WriteLine($"    - Hero: {hero!.Name}, {hero!.Type} (HP: {hero!.Health}, ATT: {hero!.Attack}, MANA: {hero!.Mana})");
            }
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

    public static void VictoryMessage()
    {
        Console.WriteLine("\n\nWith the last of the lurking monsters slain, the dungeon falls silent.");
        Console.WriteLine("Amid the scattered bones and broken weapons, you find a glimmering treasure — the legendary Diamond Ore.");
        Console.WriteLine("\nYour quest is complete. You clutch the precious diamonds tightly, feeling their weight in your hands.");
        Console.WriteLine("Visions of tavern cheers and the admiring gazes of ladies fill your mind.");
        Console.WriteLine("\nVictory is yours! Now, with your hard-earned fortune, it's time to head back to town...");
        Console.WriteLine("...and buy some beer for the ladies.");
        Console.WriteLine("\n\nCongratulations, brave hero! You've conquered the dungeon and won glory!\n\n");
        Console.WriteLine(@"
 _________  ___  ___  _______           _______   ________   ________     
|\___   ___\\  \|\  \|\  ___ \         |\  ___ \ |\   ___  \|\   ___ \    
\|___ \  \_\ \  \\\  \ \   __/|        \ \   __/|\ \  \\ \  \ \  \_|\ \   
     \ \  \ \ \   __  \ \  \_|/__       \ \  \_|/_\ \  \\ \  \ \  \ \\ \  
      \ \  \ \ \  \ \  \ \  \_|\ \       \ \  \_|\ \ \  \\ \  \ \  \_\\ \ 
       \ \__\ \ \__\ \__\ \_______\       \ \_______\ \__\\ \__\ \_______\
        \|__|  \|__|\|__|\|_______|        \|_______|\|__| \|__|\|_______|
            ");
    }

    public static void YouDiedMessage()
    {
        Console.WriteLine("\n");
        Console.WriteLine(@"
  ___    ___ ________  ___  ___          ________  ___  _______   ________     
 |\  \  /  /|\   __  \|\  \|\  \        |\   ___ \|\  \|\  ___ \ |\   ___ \    
 \ \  \/  / | \  \|\  \ \  \\\  \       \ \  \_|\ \ \  \ \   __/|\ \  \_|\ \   
  \ \    / / \ \  \\\  \ \  \\\  \       \ \  \ \\ \ \  \ \  \_|/_\ \  \ \\ \  
   \/  /  /   \ \  \\\  \ \  \\\  \       \ \  \_\\ \ \  \ \  \_|\ \ \  \_\\ \ 
 __/  / /      \ \_______\ \_______\       \ \_______\ \__\ \_______\ \_______\
|\___/ /        \|_______|\|_______|        \|_______|\|__|\|_______|\|_______|
\|___|/                                                                        
            ");
    }

    public static void GameNameMessage()
    {
        Console.WriteLine(@"
 ________  ________  ________   ________  ________  ___       _______           ________  ________  _______
|\   ____\|\   __  \|\   ___  \|\   ____\|\   __  \|\  \     |\  ___ \         |\   __  \|\   ____\|\  ___ \
\ \  \___|\ \  \|\  \ \  \\ \  \ \  \___|\ \  \|\  \ \  \    \ \   __/|        \ \  \|\  \ \  \___|\ \   __/|
 \ \  \    \ \  \\\  \ \  \\ \  \ \_____  \ \  \\\  \ \  \    \ \  \_|/__       \ \   __  \ \  \  __\ \  \_|/__
  \ \  \____\ \  \\\  \ \  \\ \  \|____|\  \ \  \\\  \ \  \____\ \  \_|\ \       \ \  \ \  \ \  \|\  \ \  \_|\ \
   \ \_______\ \_______\ \__\\ \__\____\_\  \ \_______\ \_______\ \_______\       \ \__\ \__\ \_______\ \_______\
    \|_______|\|_______|\|__| \|__|\_________\|_______|\|_______|\|_______|        \|__|\|__|\|_______|\|_______|
                                  \|_________|
 ___  ________   ________  ___  ___  ___  ________  ___  _________  ___  ________  ________
|\  \|\   ___  \|\   __  \|\  \|\  \|\  \|\   ____\|\  \|\___   ___\\  \|\   __  \|\   ___  \
\ \  \ \  \\ \  \ \  \|\  \ \  \\\  \ \  \ \  \___|\ \  \|___ \  \_\ \  \ \  \|\  \ \  \\ \  \
 \ \  \ \  \\ \  \ \  \\\  \ \  \\\  \ \  \ \_____  \ \  \   \ \  \ \ \  \ \  \\\  \ \  \\ \  \
  \ \  \ \  \\ \  \ \  \\\  \ \  \\\  \ \  \|____|\  \ \  \   \ \  \ \ \  \ \  \\\  \ \  \\ \  \
   \ \__\ \__\\ \__\ \_____  \ \_______\ \__\____\_\  \ \__\   \ \__\ \ \__\ \_______\ \__\\ \__\
    \|__|\|__| \|__|\|___| \__\|_______|\|__|\_________\|__|    \|__|  \|__|\|_______|\|__| \|__|
                          \|__|             \|_________|
            ");
    }
}