using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Commands;

public class GoCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public GoCommand(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Specify the direction.");
            return;
        }

        var direction = args[0];
        var currentRoom = _dungeon.Rooms.Find(room => room.Hero != null);
        var nextRoom = new Room();

        if (currentRoom != null && currentRoom.Enemies != null && currentRoom.Enemies.Count > 0)
        {
            Console.WriteLine("You can't go to another room if there are enemies around.");
            return;
        }

        // When player chooses next room and chooses e.g. EAST then the next room will
        // be the one which WEST door has same Id as EAST doo of the previous room
        int? doorId = new int();
        if (string.Equals(direction, "west", StringComparison.InvariantCultureIgnoreCase) && currentRoom.WestDoorId.HasValue)
        {
            doorId = currentRoom.WestDoorId;
            nextRoom = _dungeon.Rooms.Find(room => room.EastDoorId == doorId);
            Console.WriteLine("You go through the door to the west. Use \"look\" command.");
        }
        else if (string.Equals(direction, "west", StringComparison.InvariantCultureIgnoreCase) && !currentRoom.WestDoorId.HasValue)
        {
            Console.WriteLine("There is no door to the west.");
            return;
        }

        if (string.Equals(direction, "north", StringComparison.InvariantCultureIgnoreCase) && currentRoom.NorthDoorId.HasValue)
        {
            doorId = currentRoom.NorthDoorId;
            nextRoom = _dungeon.Rooms.Find(room => room.SouthDoorId == doorId);
            Console.WriteLine("You go through the door to the north. Use \"look\" command.");
        }
        else if (string.Equals(direction, "north", StringComparison.InvariantCultureIgnoreCase) && !currentRoom.NorthDoorId.HasValue)
        {
            Console.WriteLine("There is no door to the north.");
            return;
        }

        if (string.Equals(direction, "east", StringComparison.InvariantCultureIgnoreCase) && currentRoom.EastDoorId.HasValue)
        {
            doorId = currentRoom.EastDoorId;
            nextRoom = _dungeon.Rooms.Find(room => room.WestDoorId == doorId);
            Console.WriteLine("You go through the door to the east. Use \"look\" command.");
        }
        else if (string.Equals(direction, "east", StringComparison.InvariantCultureIgnoreCase) && !currentRoom.EastDoorId.HasValue)
        {
            Console.WriteLine("There is no door to the east.");
            return;
        }

        if (string.Equals(direction, "south", StringComparison.InvariantCultureIgnoreCase) && currentRoom.SouthDoorId.HasValue)
        {
            doorId = currentRoom.SouthDoorId;
            nextRoom = _dungeon.Rooms.Find(room => room.NorthDoorId == doorId);
            Console.WriteLine("You go through the door to the south. Use \"look\" command.");
        }
        else if (string.Equals(direction, "south", StringComparison.InvariantCultureIgnoreCase) && !currentRoom.SouthDoorId.HasValue)
        {
            Console.WriteLine("There is no door to the south.");
            return;
        }

        if (doorId == null || nextRoom == null)
        {
            Console.WriteLine("Something went wrong. Try again.");
        }
        else
        {
            MoveHero((int)doorId, currentRoom, nextRoom);
        }
    }

    private static void MoveHero(int doorId, Room currentRoom, Room nextRoom)
    {
        var hero = currentRoom.Hero;
        nextRoom.Hero = hero;
        currentRoom.Hero = null;
    }
}