namespace ConsoleAgeInquisition.Models;

public class Room
{
    public string RoomName;

    public List<Enemy> Enemies;

    /// Prawdopodobnie to będzie niepotrzebne, ewentualnie zamienić poszczególne drzwi poniżej na jkieś dictionary czy coś
    public List<int> Doors;

    public int? LeftDoorId;

    public int? MiddleDoorId;

    public int? ReturnDoorId;

    public int? RightDoorId;

    public Hero? Hero;

    public Chest? Chest;
}