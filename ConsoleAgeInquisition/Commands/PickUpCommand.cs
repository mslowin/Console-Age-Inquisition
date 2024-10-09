using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Commands;

public class PickUpCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public PickUpCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);

        if (args.Length == 0)
        {
            Console.WriteLine("Specify the item to pick up.");
            return;
        }

        var itemName = args[0];

        var item = currentRoom.ItemsOnTheFloor.Find(e => e.Name == itemName);

        if (item == null)
        {
            Console.WriteLine("No such item to pick up.");
            return;
        }

        // Picking up an item (removing it from ItemsOnTheFloor)
        currentRoom.ItemsOnTheFloor.Remove(item);
        var unequippedItem = currentRoom.Hero.PickUpItem(item);
        Console.WriteLine($"You picked up {item.Name}!");

        if (unequippedItem != null)
        {
            // When an old item is being unequipped, then it needs to go on the floor
            currentRoom.ItemsOnTheFloor.Add(unequippedItem);
            Console.WriteLine($"You unequipped {unequippedItem.Name}!");
        }
    }
}