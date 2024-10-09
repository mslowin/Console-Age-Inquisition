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
        ////string target = args.Length > 0 ? args[0] : "unknown";
        ////Console.WriteLine($"Attacking {target}...");

        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);

        if (args.Length == 0)
        {
            Console.WriteLine("Specify the enemy to attack.");
            return;
        }

        var enemyName = args[0];

        var enemy = currentRoom.Enemies.Find(e => e.Name == enemyName);

        if (enemy == null)
        {
            Console.WriteLine("No such enemy to attack.");
            return;
        }

        // For now, for testing it is one hit kill for every enemy
        enemy.Health -= 1000;
        Console.WriteLine($"You attack {enemy.Name}!");

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"{enemy.Name} is dead!");
            currentRoom.Enemies.Remove(enemy);

            //TODO: do zastanowienia czy nie lepiej, żeby przeciwnik po zabiciu
            //TODO: dropił skrzynie jedną z tymi rzeczami
            //TODO: wtedy w komendzie look byłoby ładniej, bo po prostu byłyby skrzynie

            // Droping enemy items on the floor
            var itemsDropped = 0;
            if (enemy.Weapon != null)
            {
                currentRoom.ItemsOnTheFloor.Add(enemy.Weapon);
                itemsDropped++;
            }
            if (enemy.Items != null)
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
        }
    }
}