using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Game
{
    public DifficultyLevel DifficultyLevel { get; set; }

    public string SaveName { get; set; }

    public Dungeon World { get; set; }
}