using ConsoleAgeInquisition.Models;
using System;

namespace ConsoleAgeInquisition.Commands;

public class ExamineCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public ExamineCommand(Dungeon dungeon)
    {
        this._dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);

        if (args.Length == 0)
        {
            Console.WriteLine("Specify the object to examine.");
            return;
        }

        var objectName = args[0];

        var enemy = currentRoom.Enemies.Find(e => e.Name == objectName);
        if (enemy != null)
        {
            ExamineEnemy(enemy);
            return;
        }

        var itemOnTheFloor = currentRoom.ItemsOnTheFloor.Find(e => e.Name == objectName);
        if (itemOnTheFloor != null)
        {
            ExamineItemOnTheFloor(itemOnTheFloor);
            return;
        }

        var chest = currentRoom.Chests.Find(e => e.Name == objectName);
        if (chest != null)
        {
            ExamineChest(chest);
            return;
        }

        if (enemy == null && itemOnTheFloor == null && chest == null)
        {
            Console.WriteLine("No such object to examine.");
        }
    }

    private static void ExamineEnemy(Enemy enemy)
    {
        Console.WriteLine($"- {enemy.Name} (HP: {enemy.Health}, ATT: {enemy.Attack}, MANA: {enemy.Mana})");
        if (enemy.Weapon != null)
        {
            Console.WriteLine("    * Weapon:");
            Console.WriteLine($"        + {enemy.Weapon.Name} (ATT buff: {enemy.Weapon.AttackBuff})");
        }

        if (enemy.HeadArmor != null || enemy.ChestArmor != null || enemy.ArmsArmor != null || enemy.LegsArmor != null)
        {
            // if enemy has any type of armor
            Console.WriteLine("    * Armor:");
        }
        if (enemy.HeadArmor != null)
        {
            Console.WriteLine($"        + Head armor: {enemy.HeadArmor} (HP buff: {enemy.HeadArmor.HealthBuff})");
        }
        if (enemy.ChestArmor != null)
        {
            Console.WriteLine($"        + Chest armor: {enemy.ChestArmor} (HP buff: {enemy.ChestArmor.HealthBuff})");
        }
        if (enemy.ArmsArmor != null)
        {
            Console.WriteLine($"        + Arms armor: {enemy.ArmsArmor} (HP buff: {enemy.ArmsArmor.HealthBuff})");
        }
        if (enemy.LegsArmor != null)
        {
            Console.WriteLine($"        + Legs armor: {enemy.LegsArmor} (HP buff: {enemy.LegsArmor.HealthBuff})");
        }

        if (enemy.Items.Count > 0)
        {
            Console.WriteLine("    * Items:");
            foreach (var item in enemy.Items)
            {
                Console.WriteLine($"        + {item.Name} (Type: {item.Type})");
            }
        }
    }

    private static void ExamineItemOnTheFloor(Item itemOnTheFloor)
    {
        Console.WriteLine($"- {itemOnTheFloor.Name} (Type: {itemOnTheFloor.Type})");
        if (itemOnTheFloor.AttackBuff > 0)
        {
            Console.WriteLine($"    * Attack buff: {itemOnTheFloor.AttackBuff}");
        }
        if (itemOnTheFloor.HealthBuff > 0)
        {
            Console.WriteLine($"    * Health buff: {itemOnTheFloor.HealthBuff}");
        }
        if (itemOnTheFloor.ManaBuff > 0)
        {
            Console.WriteLine($"    * Mana buff: {itemOnTheFloor.ManaBuff}");
        }
    }

    private static void ExamineChest(Chest chest)
    {
        Console.WriteLine($"- {chest.Name}");
        Console.WriteLine("    * Items:");
        foreach (var item in chest.Items)
        {

            Console.WriteLine($"        + {item.Name} (Type: {item.Type})");
        }
    }
}