using ConsoleAgeInquisition.Models;
using System.Xml.Linq;
using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition;

public static class Resources
{
    public static Item GetDiamondOre()
    {
        return new Item
        {
            Type = ItemType.QuestItem,
            Name = "DiamondOre",
            AttackBuff = 0,
            HealthBuff = 0,
            ManaBuff = 0,
        };
    } 

    public static string GetGameSavesFolderPath()
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../GameSaves"));
    }
}
