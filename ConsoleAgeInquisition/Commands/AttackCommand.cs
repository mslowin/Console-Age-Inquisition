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
        }
        else
        {
            Console.WriteLine($"{enemy.Name} has {enemy.Health} HP left.");
        }
    }
}