using ConsoleAgeInquisition.Enums;
using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Services;

/// <summary>
/// Class containing methods for creating entities.
/// </summary>
public static class EntitiesService
{
    /// <summary>
    /// Creates a new instance of the Item class with the provided parameters.
    /// </summary>
    /// <param name="type">The type of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="attackBuff">The attack buff provided by the item.</param>
    /// <param name="healthBuff">The health buff provided by the item.</param>
    /// <param name="manaBuff">The mana buff provided by the item.</param>
    /// <returns>A new Item instance with the provided parameters.</returns>

    public static Item CreateItem(ItemType type, string name, int attackBuff, int healthBuff, int manaBuff)
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

    /// <summary>
    /// Creates a new instance of the Enemy class with the provided parameters.
    /// </summary>
    /// <param name="health">The initial health of the enemy.</param>
    /// <param name="attack">The attack power of the enemy.</param>
    /// <param name="mana">The mana points of the enemy.</param>
    /// <param name="name">The name of the enemy.</param>
    /// <param name="type">The type of the enemy.</param>
    /// <param name="weapon">The weapon the enemy is using.</param>
    /// <param name="items">A list of items the enemy is carrying.</param>
    /// <returns>A new Enemy instance with the provided parameters.</returns>

    public static Enemy CreateEnemy(int health, int attack, int mana, string name, CharacterType type, Weapon weapon, List<Item> items)
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

    /// <summary>
    /// Creates rooms with one middle door and one return door. The rooms are all in a row.
    /// </summary>
    /// <param name="iteration">iteration of room creation.</param>
    /// <param name="enemies">List of enemies for this room.</param>
    /// <returns></returns>
    public static Room CreateRoomEasyMode(int iteration, List<Enemy> enemies)
    {
        var chests = new List<Chest>();
        Chest? chest = null;

        if (iteration == 0)
        {
            chest = new Chest { Name = "Chest1", Items = new List<Item> { new Weapon { AttackBuff = 10, Name = "Razor" } } };
        }

        if (iteration == 5)
        {
            chest = new Chest { Name = "Chest2", Items = new List<Item> { new Armor { HealthBuff = 10, Name = "HelmetOfUndying", ArmorType = ArmorType.Helmet } } };
        }

        if (chest != null)
        {
            chests.Add(chest);
        }

        return new Room
        {
            Chests = chests,
            RoomName = $"Room{iteration}",
            NorthDoorId = iteration,
            SouthDoorId = iteration - 1 < 0 ? null : iteration - 1,
            Enemies = enemies,
            ItemsOnTheFloor = new List<Item>()
        };
    }

    /// <summary>
    /// Creates a list of weak enemies.
    /// </summary>
    /// <param name="numOfEnemies">The number of enemies to create.</param>
    /// <returns>A list of enemies.</returns>
    public static List<Enemy> CreateWeakEnemies(int numOfEnemies)
    {
        var enemies = new List<Enemy>();
        for (var i = 0; i < numOfEnemies; i++)
        {
            var enemy = CreateEnemy(100, 5, 0, $"Bob{i}", CharacterType.Goblin,
                new Weapon { Type = ItemType.Weapon, AttackBuff = 2, Name = "Stick" },
                new List<Item>
                {
                    CreateItem(ItemType.PowerRing, "RingOfPower", 0, 5, 0)
                });
            enemies.Add(enemy);
        }

        return enemies;
    }

    /// <summary>
    /// Creates a weak boss for easy game mode.
    /// </summary>
    /// <param name="name">Name of the boss.</param>
    /// <returns>A list of enemies.</returns>
    public static Enemy CreateWeakBoss(string name)
    {
            var enemy = CreateEnemy(200, 10, 0, name, CharacterType.Goblin,
                new Weapon { Type = ItemType.Weapon, AttackBuff = 5, Name = "GiantStick" },
                new List<Item>
                {
                    CreateItem(ItemType.PowerRing, "RingOfHappiness", 0, 5, 5),
                    Resources.GetDiamondOre()
                });

        return enemy;
    }

    /// <summary>
    /// Creates a list of simple rooms where every room has only one enemy.
    /// </summary>
    /// <param name="numOfRooms">The number of rooms to create.</param>
    /// <param name="enemies">List of enemies (one enemy per room).</param>
    /// <returns>A list of rooms.</returns>
    public static List<Room> CreateSimpleRooms(int numOfRooms, List<Enemy> enemies)
    {
        var rooms = new List<Room>();
        for (var i = 0; i < numOfRooms; i++)
        {
            var room = CreateRoomEasyMode(i, new List<Enemy> { enemies[i] });
            rooms.Add(room);
        }

        return rooms;
    }
}