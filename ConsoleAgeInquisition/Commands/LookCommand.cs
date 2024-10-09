using ConsoleAgeInquisition.Models;

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

                // TODO: To będzie do examine, albo do wchodzenia w tryb atakowania, czy coś:
                ////Console.WriteLine($"- {enemy.Name} (HP: {enemy.Health}, ATT: {enemy.Attack}, MANA: {enemy.Mana})");
                ////if (enemy.Weapon != null)
                ////{
                ////    Console.WriteLine($"    * Weapon: {enemy.Weapon}");
                ////}
                ////if (enemy.ChestArmor != null || enemy.HeadArmor != null || enemy.ArmsArmor != null ||
                ////    enemy.LegsArmor != null)
                ////{
                ////}
                ////Console.WriteLine($"    * Items: {enemy.Weapon}");
            }
        }
        else
        {
            Console.WriteLine("No enemies present.");
        }

        // Showing chests
        if (currentRoom.Chest != null)
        {
            Console.WriteLine($"\nThere is a chest here. It contains: {currentRoom.Chest.Items}");
        }
        else
        {
            Console.WriteLine("\nNo chest in the room.");
        }

        // Showing doors
        Console.WriteLine("\nDoors:");
        if (currentRoom.MiddleDoorId.HasValue) Console.WriteLine($"- Middle door");
        if (currentRoom.LeftDoorId.HasValue) Console.WriteLine($"- Left door");
        if (currentRoom.RightDoorId.HasValue) Console.WriteLine($"- Right door");
        if (currentRoom.ReturnDoorId.HasValue) Console.WriteLine($"- Return door");
    }
}