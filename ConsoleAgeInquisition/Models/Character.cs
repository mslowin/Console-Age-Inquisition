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

    public void AdjustStatsByItems()
    {
        if (Weapon != null)
        {
            Health += Weapon.HealthBuff;
            Attack += Weapon.AttackBuff;
            Mana += Weapon.ManaBuff;
        }

        if (HeadArmor != null)
        {
            Health += HeadArmor.HealthBuff;
            Attack += HeadArmor.AttackBuff;
            Mana += HeadArmor.ManaBuff;
        }

        if (ChestArmor != null)
        {
            Health += ChestArmor.HealthBuff;
            Attack += ChestArmor.AttackBuff;
            Mana += ChestArmor.ManaBuff;
        }

        if (ArmsArmor != null)
        {
            Health += ArmsArmor.HealthBuff;
            Attack += ArmsArmor.AttackBuff;
            Mana += ArmsArmor.ManaBuff;
        }

        if (LegsArmor != null)
        {
            Health += LegsArmor.HealthBuff;
            Attack += LegsArmor.AttackBuff;
            Mana += LegsArmor.ManaBuff;
        }

        if (Items.Count <= 0)
        {
            return;
        }

        foreach (var item in Items.Where(item => item.Type == ItemType.PowerRing))
        {
            Health += item.HealthBuff;
            Attack += item.AttackBuff;
            Mana += item.ManaBuff;
        }
    }

    /// <summary>
    /// Sets characters health, attack and mana based on the character type
    /// </summary>
    public void SetHealthAttackManaByType()
    {
        switch (Type)
        {
            case CharacterType.Human:
                Health = 100;
                Attack = 50;
                Mana = 40;
                break;

            case CharacterType.Elf:
                Health = 90;
                Attack = 45;
                Mana = 70;
                break;

            case CharacterType.Dwarf:
                Health = 110;
                Attack = 55;
                Mana = 20;
                break;

            case CharacterType.Orc:
                Health = 120;
                Attack = 65;
                Mana = 10;
                break;

            case CharacterType.Goblin:
                Health = 80;
                Attack = 60;
                Mana = 30;
                break;

            default:
                Health = 70;
                Attack = 40;
                Mana = 40;
                break;
        }
    }
}