using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Services;

public static class ViewsService
{
    public static int HandleMainMenu()
    {
        int choice;
        Console.WriteLine("Welcome to Console Age: Inquisition");
        Console.WriteLine("Please let me know what do you want me to do:");
        Console.WriteLine("1. Start a new game");
        Console.WriteLine("2. Load game");
        Console.WriteLine("3. Exit");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice is 1 or 2 or 3)
                {
                    break;
                }

                Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
            }

            Console.WriteLine("Invalid input. Please enter a number.");
        }

        return choice;
    }

    public static string HandleDifficultyLevelMenu()
    {
        string? difficultyLevel;
        Console.WriteLine("Select difficulty level for the game:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        while (true)
        {
            difficultyLevel = Console.ReadLine();
            if (difficultyLevel != null
                && difficultyLevel.Length > 0
                && difficultyLevel is "1" or "2" or "3")
            {
                break;
            }

            Console.WriteLine("Invalid difficulty level. Please choose 1, 2, or 3.");
        }

        return difficultyLevel;
    }

    public static string HandleHeroTypeMenu()
    {
        string? heroType;
        Console.WriteLine("Chose Character Type:");
        foreach (var type in Enum.GetValues(typeof(CharacterType)))
        {
            Console.WriteLine($"{(int)type}. {type}");
        }
        while (true)
        {
            heroType = Console.ReadLine();
            if (int.TryParse(heroType, out var heroTypeInt) && Enum.IsDefined(typeof(CharacterType), heroTypeInt))
            {
                break;
            }

            Console.WriteLine("Invalid type. Please provide a valid type.");
        }

        return heroType;
    }
}