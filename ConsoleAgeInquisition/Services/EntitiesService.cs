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
    /// <param name="type">The type of the item (e.g., Weapon, Armor, Consumable).</param>
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
    /// <param name="type">The type of the enemy (e.g., Warrior, Mage, Rogue).</param>
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
        Chest? chest = null;

        if (iteration == 2)
        {
            chest = new Chest { Items = new List<Item> { new Weapon { AttackBuff = 10, Name = "Razor" } } };
        }

        if (iteration == 5)
        {
            chest = new Chest { Items = new List<Item> { new Armor { HealthBuff = 10, Name = "Helmet of undying", ArmorType = ArmorType.Helmet } } };
        }

        return new Room
        {
            Chest = chest,
            RoomName = $"Room{iteration}",
            MiddleDoorId = iteration,
            ReturnDoorId = iteration - 1,
            Doors = new List<int> { 1 },
            Enemies = enemies
        };
    }
}