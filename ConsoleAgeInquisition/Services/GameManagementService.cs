using ConsoleAgeInquisition.Enums;
using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Services;

/// <summary>
/// Service for methods connected with creating a new game and loading already existent.
/// </summary>
public static class GameManagementService
{
    /// <summary>
    /// Starts the whole game.
    /// </summary>
    public static void Start()
    {
        var choice = ViewsService.HandleMainMenu();

        if (choice == 1)
        {
            var game = CreateNewGame();
            game.World.Rooms[0].Hero = CreateHero();

            GameService.Run(game);
        }

        if (choice == 2)
        {
            // TODO: wczytywanie gry, ale wcześniej zapisywanie jej
        }
    }

    private static Game CreateNewGame()
    {
        var difficultyLevel = ViewsService.HandleDifficultyLevelMenu();

        var saveName = ViewsService.HandleGameNameSelection();

        // TODO: switch na poziom trudności
        // TODO: jeśli gra łatwa: 6 pokoi, każdy pokój 1 przeciwnik (mało życia i ataku), 2 skrzynie, 1 boss (łatwy)
        // TODO: jeśli gra średnia: ...
        // TODO: jeśli gra trudna: ...

        // Constants for easy game mode
        var numOfEnemies = 6;
        var numOfRooms = 6;

        // Creating enemies for easy game mode
        var enemies = EntitiesService.CreateWeakEnemies(numOfEnemies);

        // Creating rooms for easy game mode
        var rooms = EntitiesService.CreateSimpleRooms(numOfRooms, enemies);

        var dungeon = new Dungeon { Rooms = rooms };

        Console.WriteLine("");
        Console.WriteLine("Game initialization finished. Time to create your hero...");

        return new Game
        {
            DifficultyLevel = (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), difficultyLevel),
            SaveName = saveName,
            World = dungeon
        };
    }

    public static Hero CreateHero()
    {
        Hero hero = new();

        hero.Name = ViewsService.HandleHeroNameSelection();

        var heroType = ViewsService.HandleHeroTypeMenu();
        hero.Type = (CharacterType)Enum.Parse(typeof(CharacterType), heroType);

        hero.Items = new List<Item>();

        hero.SetHealthAttackMana();

        return hero;
    }
}