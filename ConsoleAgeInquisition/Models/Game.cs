using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Game
{
    public DifficultyLevel DifficultyLevel { get; set; }

    public string SaveName { get; set; }

    public Dungeon Dungeon { get; set; }
}