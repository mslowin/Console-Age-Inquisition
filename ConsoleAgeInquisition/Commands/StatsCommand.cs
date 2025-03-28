using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Commands;

public class StatsCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public StatsCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);
        var hero = currentRoom?.Hero;

        if (hero == null)
        {
            Console.WriteLine("Something went wrong. Try again.");
            return;
        }

        WriteHeroStats(hero);
    }

    private static void WriteHeroStats(Hero hero)
    {
        Console.WriteLine($"- Name: {hero.Name} (HP: {hero.Health}, ATT: {hero.Attack}, MANA: {hero.Mana})");
        if (hero.Weapon != null)
        {
            Console.WriteLine("    * Weapon:");
            Console.WriteLine($"        + {hero.Weapon.Name} (ATT buff: {hero.Weapon.AttackBuff})");
        }
        else
        {
            Console.WriteLine("    * No weapon equipped.");
        }

        if (hero.HeadArmor != null || hero.ChestArmor != null || hero.ArmsArmor != null || hero.LegsArmor != null)
        {
            // if hero has any type of armor
            Console.WriteLine("    * Armor:");
        }
        else
        {
            Console.WriteLine("    * No armor equipped.");
        }

        if (hero.HeadArmor != null)
        {
            Console.WriteLine($"        + Head armor: {hero.HeadArmor.Name} (HP buff: {hero.HeadArmor.HealthBuff})");
        }
        if (hero.ChestArmor != null)
        {
            Console.WriteLine($"        + Chest armor: {hero.ChestArmor.Name} (HP buff: {hero.ChestArmor.HealthBuff})");
        }
        if (hero.ArmsArmor != null)
        {
            Console.WriteLine($"        + Arms armor: {hero.ArmsArmor.Name} (HP buff: {hero.ArmsArmor.HealthBuff})");
        }
        if (hero.LegsArmor != null)
        {
            Console.WriteLine($"        + Legs armor: {hero.LegsArmor.Name} (HP buff: {hero.LegsArmor.HealthBuff})");
        }

        if (hero.Items.Count > 0)
        {
            Console.WriteLine("    * Items:");
            foreach (var item in hero.Items)
            {
                Console.WriteLine($"        + {item.Name} (Type: {item.Type} HP buff: {item.HealthBuff}, " +
                                  $"ATT buff: {item.AttackBuff}, MANA buff: {item.ManaBuff})");
            }
        }
        else
        {
            Console.WriteLine("    * No items in the backpack.");
        }
    }
}