using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Hero : Character
{
    // TODO: tu wszystko do przetestowania

    // TODO: dokończyć podnoszenie innych części armoru

    /// <summary>
    /// Picks up an weapon and adjusts the buffs.
    /// </summary>
    /// <param name="item">Item to equip.</param>
    /// <returns>Item that is removed. Should be placed on the ground (Items on the floor).</returns>
    public Item? PickUpItem(Item item)
    {
        if (item.Type == ItemType.Weapon)
        {
            return PickUpWeapon(item);
        }

        if (item.Type == ItemType.Armor)
        {
            // If the armor is a weapon
            if (((Armor)item).ArmorType == ArmorType.Helmet)
            {
                return PickUpHelmet(item);
            }
        }

        return null;
    }

    private Item? PickUpWeapon(Item weapon)
    {
        // If the hero already has a weapon, change it to new one
        if (Weapon != null)
        {
            // Removing old weapon buffs and unequipping old weapon
            Health -= Weapon.HealthBuff;
            Attack -= Weapon.AttackBuff;
            Mana -= Weapon.ManaBuff;
            var oldWeapon = Weapon;

            // Adding new weapon buffs and equiping the new weapon
            Health += weapon.HealthBuff;
            Attack += weapon.AttackBuff;
            Mana += weapon.ManaBuff;
            Weapon = (Weapon)weapon;

            return oldWeapon;
        }
        else
        {
            // Adding new weapon buffs and equiping the new weapon
            Weapon = (Weapon)weapon;

            Health += weapon.HealthBuff;
            Attack += weapon.AttackBuff;
            Mana += weapon.ManaBuff;

            return null;
        }
    }

    private Item? PickUpHelmet(Item helmet)
    {
        // If the hero already has a weapon, change it to new one
        if (HeadArmor != null)
        {
            // Removing old armor buffs and unequipping old armor
            Health -= HeadArmor.HealthBuff;
            Attack -= HeadArmor.AttackBuff;
            Mana -= HeadArmor.ManaBuff;
            var oldArmor = HeadArmor;

            // Adding new armor buffs and equiping the new armor
            Health += helmet.HealthBuff;
            Attack += helmet.AttackBuff;
            Mana += helmet.ManaBuff;
            HeadArmor = (Armor)helmet;

            return oldArmor;
        }
        else
        {
            // Adding new armor buffs and equiping the new armor
            HeadArmor = (Armor)helmet;

            Health += helmet.HealthBuff;
            Attack += helmet.AttackBuff;
            Mana += helmet.ManaBuff;

            return null;
        }
    }
}