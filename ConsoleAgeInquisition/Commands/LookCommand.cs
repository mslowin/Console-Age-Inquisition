using ConsoleAgeInquisition.Models;
using System;

namespace ConsoleAgeInquisition.Commands;

public class LookCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public LookCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        // The room where the hero is
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);
        Console.WriteLine($"You are now in: {currentRoom.RoomName}\n");

        // Showing enemies
        if (currentRoom.Enemies.Count > 0)
        {
            Console.WriteLine("Enemies in the room:");
            foreach (var enemy in currentRoom.Enemies)
            {
                Console.WriteLine($"- {enemy.Type} {enemy.Name} (HP: {enemy.Health})");
            }
        }
        else
        {
            Console.WriteLine("No enemies present.");
        }

        // Showing chests
        if (currentRoom.Chests.Count > 0)
        {
            Console.WriteLine("\nChests in the room:");
            foreach (var item in currentRoom.Chests)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }
        else
        {
            Console.WriteLine("\nNo chests in the room.");
        }

        // Showing items on the floor
        if (currentRoom.ItemsOnTheFloor.Count > 0)
        {
            Console.WriteLine("\nItems on the floor:");
            foreach (var item in currentRoom.ItemsOnTheFloor)
            {
                Console.WriteLine($"- {item.Name} (Type: {item.Type})");
            }
        }
        else
        {
            Console.WriteLine("\nNo items on the floor.");
        }

        // Showing doors
        Console.WriteLine("\nDoors:");
        if (currentRoom.MiddleDoorId.HasValue) Console.WriteLine($"- Middle door");
        if (currentRoom.LeftDoorId.HasValue) Console.WriteLine($"- Left door");
        if (currentRoom.RightDoorId.HasValue) Console.WriteLine($"- Right door");
        if (currentRoom.ReturnDoorId.HasValue) Console.WriteLine($"- Return door");
    }
}