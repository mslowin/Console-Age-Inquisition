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
            // TODO: enum drzwi: frontDoor, LEftDoor, itd
            // TODO: enum poziomów trudności gry
            var game = HandleNewGameMenu();
            // TODO: ------------------------------------->
            // When game is ready, The character needs to be initialized and added to one of the rooms
            // Consider adding separate fields for doors in a room like FirstDoorId and in the other Room the Ids
            // would be the same so the code would now where the character went (what room)
        }

        ////return Console.ReadLine();
    }

    private static Game HandleNewGameMenu()
    {
        Console.WriteLine("Select difficulty level for the game:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");

        var difficultyLevel = Console.ReadLine();
        while (difficultyLevel != "1" && difficultyLevel != "2" && difficultyLevel != "3")
        {
            Console.WriteLine("Invalid difficulty level. Please choose 1, 2 or 3.");
            difficultyLevel = Console.ReadLine();
        }

        Console.WriteLine("Type a name for this game:");
        var saveName = Console.ReadLine();

        while (string.IsNullOrEmpty(saveName))
        {
            Console.WriteLine("Invalid save name. Please provide a valid name.");
            saveName = Console.ReadLine();
        }

        // TODO: jeśli gra łatwa: 6 pokoi, każdy pokój 1 przeciwnik (mało życia i ataku), 2 skrzynie, 1 boss (łatwy)
        // ...
        // ...

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
            var room = CreateRoom(1, new List<Enemy> { enemies[i] });
            rooms.Add(room);
        }

        var dungeon = new Dungeon { Rooms = rooms };

        return new Game { DifficultyLevel = difficultyLevel, SaveName = saveName, World = dungeon };
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

    private static Room CreateRoom(int numOfDoors, List<Enemy> enemies)
    {
        var doorsList = new List<int>();
        for (int i = 1; i <= numOfDoors; i++)
        {
            doorsList.Add(i);
        }

        return new Room
        {
            Doors = doorsList,
            Enemies = enemies
        };
    }
}
