using ConsoleAgeInquisition.Enums;
using ConsoleAgeInquisition.Models;

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
        Console.WriteLine("Choose Character Type:");
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
}