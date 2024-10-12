using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Commands;

public class OpenCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public OpenCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Specify the chest to open.");
            return;
        }

        var chestName = args[0];
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);
        var chest = currentRoom!.Chests.Find(chest => chest.Name == chestName);

        if (chest == null)
        {
            Console.WriteLine("There is no chest with given name in this room.");
            return;
        }

        if (currentRoom.Enemies.Count > 0)
        {
            Console.WriteLine("You can't open chests when there are enemies nearby.");
            return;
        }

        chest.Open(currentRoom);
        Console.WriteLine("Chest has been opened and the items were thrown on the ground. Use \"look\" command");
    }
}