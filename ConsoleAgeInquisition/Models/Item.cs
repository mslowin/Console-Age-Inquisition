using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Item
{
    public ItemType Type { get; set; }

    public string Name;

    public int AttackBuff;

    public int HealthBuff;

    public int ManaBuff;
}