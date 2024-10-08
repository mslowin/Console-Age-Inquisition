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
            // TODO: enum poziomów trudności gry
            var game = CreateNewGame();
            game.World.Rooms[0].Hero = CreateHero();

            GameService.Run(game);
        }

        if (choice == 2)
        {
            // Wczytywanie
        }
    }

    private static Game CreateNewGame()
    {
        var difficultyLevel = ViewsService.HandleDifficultyLevelMenu();

        Console.WriteLine("Type a name for this game:");
        var saveName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(saveName))
        {
            Console.WriteLine("Invalid save name. Please provide a valid name.");
            saveName = Console.ReadLine();
        }

        // TODO: switch na poziom trudności
        // TODO: jeśli gra łatwa: 6 pokoi, każdy pokój 1 przeciwnik (mało życia i ataku), 2 skrzynie, 1 boss (łatwy)
        // TODO: jeśli gra średnia: ...
        // TODO: jeśli gra trudna: ...

        // Constants for easy game mode
        var numOfEnemies = 6;
        var enemies = new List<Enemy>();
        var rooms = new List<Room>();

        // Creating enemies for easy game mode
        for (var i = 0; i < numOfEnemies; i++)
        {
            var enemy = EntitiesService.CreateEnemy(100, 50, 0, $"Bob{i}", CharacterType.Goblin,
                new Weapon { Type = ItemType.Weapon, AttackBuff = 2, Name = "Stick" },
                new List<Item> { EntitiesService.CreateItem(ItemType.PowerRing, "Ring of never ending happiness", 0, 5, 0) });
            enemies.Add(enemy);
        }

        // Creating rooms for easy game mode
        for (var i = 0; i < numOfEnemies; i++)
        {
            var room = EntitiesService.CreateRoomEasyMode(i, new List<Enemy> { enemies[i] });
            rooms.Add(room);
        }

        var dungeon = new Dungeon { Rooms = rooms };

        Console.WriteLine("");
        Console.WriteLine("Game initialization finished. Time to create your hero...");

        return new Game { DifficultyLevel = difficultyLevel, SaveName = saveName, World = dungeon };
    }

    public static Hero CreateHero()
    {
        var hero = new Hero();

        #region HeroName

        Console.WriteLine("Type a name for the hero:");
        var heroName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(heroName))
        {
            Console.WriteLine("Invalid name. Please provide a valid name.");
            heroName = Console.ReadLine();
        }
        hero.Name = heroName;

        #endregion HeroName

        var heroType = ViewsService.HandleHeroTypeMenu();
        hero.Type = (CharacterType)Enum.Parse(typeof(CharacterType), heroType);

        hero.Items = new List<Item>();

        hero.SetHealthAttackMana();

        return hero;
    }
}