namespace ConsoleAgeInquisition.Models;

public class Room
{
    public string RoomName;

    public List<Enemy> Enemies;

    public int? LeftDoorId;

    public int? MiddleDoorId;

    public int? ReturnDoorId;

    public int? RightDoorId;

    public Hero? Hero;

    public List<Chest> Chests;

    public List<Item> ItemsOnTheFloor;
}