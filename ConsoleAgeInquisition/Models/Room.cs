namespace ConsoleAgeInquisition.Models;

public class Room
{
    public string RoomName;

    public List<Enemy> Enemies = new();

    public int? WestDoorId;

    public int? NorthDoorId;

    public int? SouthDoorId;

    public int? EastDoorId;

    public Hero? Hero;

    public List<Chest> Chests = new();

    public List<Item> ItemsOnTheFloor = new();
}