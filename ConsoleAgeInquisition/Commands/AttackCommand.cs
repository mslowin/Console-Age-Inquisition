using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Commands;

public class AttackCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public AttackCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Specify the enemy to attack.");
            return;
        }

        var enemyName = args[0];
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);
        var enemy = currentRoom.Enemies.Find(e => e.Name == enemyName);

        if (enemy == null)
        {
            Console.WriteLine("No such enemy to attack.");
            return;
        }

        AttackEnemy(enemy, currentRoom);
    }

    private static void AttackEnemy(Enemy enemy, Room currentRoom)
    {
        // For now, for testing it is one hit kill for every enemy
        var damageDealt = currentRoom.Hero!.Attack;
        enemy.Health -= damageDealt;
        Console.WriteLine($"You attack {enemy.Name} and deal {damageDealt} damage!");

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"{enemy.Name} is dead!");
            currentRoom.Enemies.Remove(enemy);

            // Dropping enemy items on the floor
            var itemsDropped = 0;
            if (enemy.Weapon != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.Weapon);
                itemsDropped++;
            }
            if (enemy.Items.Count > 0)
            {
                currentRoom.ItemsOnTheFloor.AddRange(enemy.Items);
                itemsDropped++;
            }
            if (enemy.HeadArmor != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.HeadArmor);
                itemsDropped++;
            }
            if (enemy.ChestArmor != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.ChestArmor);
                itemsDropped++;
            }
            if (enemy.LegsArmor != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.LegsArmor);
                itemsDropped++;
            }
            if (enemy.ArmsArmor != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.ArmsArmor);
                itemsDropped++;
            }

            if (itemsDropped > 0)
            {
                Console.WriteLine($"{enemy.Name} dropped his items! (use \"look\" command)");
            }
        }
        else
        {
            Console.WriteLine($"{enemy.Name} has {enemy.Health} HP left.");

            // Enemy attacks back
            var damageDealtToHero = enemy.AttackHero();
            currentRoom.Hero!.Health -= damageDealtToHero;
            Console.WriteLine(damageDealtToHero > 0
                ? $"{enemy.Name} attacks you and deals {damageDealtToHero} damage!"
                : $"{enemy.Name} tries to attack you but misses!");
        }
    }
}