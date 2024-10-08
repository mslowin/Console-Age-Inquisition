using ConsoleAgeInquisition.Enums;
using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Services;

public static class ViewsService
{
    public static void HandleMainMenu()
    {
        Console.WriteLine("Welcome to Console Age: Inquisition");
        Console.WriteLine("Please let me know what do you want me to do:");
        Console.WriteLine("1. Start a new game");
        Console.WriteLine("2. Load game");
        Console.WriteLine("3. Exit");

        var choice = Console.ReadLine();

        if (choice == "1")
        {
            // TODO: enum poziomów trudności gry
            var game = HandleNewGameMenu();
            game = HandleHeroCreationMenu(game);
            // TODO: ------------------------------------->
            // When game is ready, The character needs to be initialized and added to one of the rooms
        }

        ////return Console.ReadLine();
    }

    private static Game HandleNewGameMenu()
    {
        Console.WriteLine("Select difficulty level for the game:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        // TODO: Console.WriteLine("4. Return");
        var difficultyLevel = Console.ReadLine();
        while (difficultyLevel != null
            && difficultyLevel.Trim() != "1"
            && difficultyLevel.Trim() != "2"
            && difficultyLevel.Trim() != "3")
        {
            Console.WriteLine("Invalid difficulty level. Please choose 1, 2 or 3.");
            difficultyLevel = Console.ReadLine();
        }

        Console.WriteLine("Type a name for this game:");
        // TODO: Console.WriteLine("Type "return" to go back");
        var saveName = Console.ReadLine();
        while (string.IsNullOrEmpty(saveName))
        {
            Console.WriteLine("Invalid save name. Please provide a valid name.");
            saveName = Console.ReadLine();
        }

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
            var enemy = CreateEnemy(100, 50, 0, $"Bob{i}", CharacterType.Goblin,
                        new Weapon { Type = ItemType.Weapon, AttackBuff = 2, Name = "Stick" },
                        new List<Item> { CreateItem(ItemType.PowerRing, "Ring of never ending happiness", 0, 5, 0) });
            enemies.Add(enemy);
        }

        // Creating rooms for easy game mode
        for (var i = 0; i < numOfEnemies; i++)
        {
            var room = CreateRoomEasyMode(i, new List<Enemy> { enemies[i] });
            rooms.Add(room);
        }

        var dungeon = new Dungeon { Rooms = rooms };

        Console.WriteLine("Game initialization finished. Time to create your hero...");
        Console.WriteLine("");

        return new Game { DifficultyLevel = difficultyLevel, SaveName = saveName, World = dungeon };
    }

    private static Game HandleHeroCreationMenu(Game game)
    {
        // TODO: whole hero creation menu
        var hero = new Hero();

        Console.WriteLine("Type a name for the hero:");
        var heroName = Console.ReadLine();
        while (string.IsNullOrEmpty(heroName))
        {
            Console.WriteLine("Invalid name. Please provide a valid name.");
            heroName = Console.ReadLine();
        }
        hero.Name = heroName;

        // ...

        game.World.Rooms[0].Hero = hero;

        return game;
    }

    private static Item CreateItem(ItemType type, string name, int attackBuff, int healthBuff, int manaBuff)
    {
        return new Item
        {
            Type = type,
            Name = name,
            AttackBuff = attackBuff,
            HealthBuff = healthBuff,
            ManaBuff = manaBuff,
        };
    }

    private static Enemy CreateEnemy(int health, int attack, int mana, string name, CharacterType type, Weapon weapon, List<Item> items)
    {
        return new Enemy
        {
            Health = health,
            Attack = attack,
            Mana = mana,
            Name = name,
            Type = type,
            Weapon = weapon,
            Items = items,
        };
    }

    /// Creates rooms with one middle door and one return door. The rooms are all in a row.
    private static Room CreateRoomEasyMode(int iteration, List<Enemy> enemies)
    {
        return new Room
        {
            RoomName = $"Room{iteration}",
            MiddleDoorId = iteration,
            ReturnDoorId = iteration - 1,
            Doors = new List<int> { 1 },
            Enemies = enemies
        };
    }
}