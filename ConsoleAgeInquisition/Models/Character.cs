using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

/// <summary>
/// Base clas for every character in the game
/// </summary>
public class Character
{
    public int Health;

    public int Attack;

    public int Mana;

    public string Name;

    public CharacterType Type;

    public Armor? HeadArmor; // kasa armor, z property typu ArmorType.Head albo ArmorType.Legs

    public Armor? ChestArmor;

    public Armor? ArmsArmor;

    public Armor? LegsArmor;

    public Weapon? Weapon;

    public List<Item> Items; // Klasa Item
}