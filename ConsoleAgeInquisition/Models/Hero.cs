using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Hero : Character
{
    /// <summary>
    /// Pick up an item and adjust the buffs.
    /// </summary>
    /// <param name="item">Item to equip.</param>
    /// <returns>Item that is removed. Should be placed on the ground (Items on the floor).</returns>
    public Item? PickUpItem(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Weapon:
            {
                return PickUpWeapon(item);
            }
            case ItemType.PowerRing:
            {
                return PickUpPowerRing(item);
            }
            case ItemType.Armor when ((Armor)item).ArmorType == ArmorType.Helmet:
            {
                return PickUpHelmet(item);
            }
            case ItemType.Armor when ((Armor)item).ArmorType == ArmorType.ChestPiece:
            {
                return PickUpChestPiece(item);
            }
            case ItemType.Armor when ((Armor)item).ArmorType == ArmorType.ArmsArmor:
            {
                return PickUpArmsArmor(item);
            }
            case ItemType.Armor when ((Armor)item).ArmorType == ArmorType.LegsArmor:
            {
                return PickUpLegsArmor(item);
            }
            case ItemType.Food:
            case ItemType.Potion:
            case ItemType.QuestItem:
            {
                Items.Add(item);
                return null;
            }
            default:
            {
                return null;
            }
        }
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

    private Item? PickUpPowerRing(Item powerRing)
    {
        // Adding new power ring to inventory and adding its buffs
        Items.Add(powerRing);

        Health += powerRing.HealthBuff;
        Attack += powerRing.AttackBuff;
        Mana += powerRing.ManaBuff;

        return null;
    }

    private Item? PickUpHelmet(Item helmet)
    {
        // If the hero already has a helmet, change it to new one
        if (HeadArmor != null)
        {
            // Removing old armor buffs and unequipping old armor
            Health -= HeadArmor.HealthBuff;
            Attack -= HeadArmor.AttackBuff;
            Mana -= HeadArmor.ManaBuff;
            var oldArmor = HeadArmor;

            // Adding new armor buffs and equipping the new armor
            Health += helmet.HealthBuff;
            Attack += helmet.AttackBuff;
            Mana += helmet.ManaBuff;
            HeadArmor = (Armor)helmet;

            return oldArmor;
        }
        else
        {
            // Adding new armor buffs and equipping the new armor
            HeadArmor = (Armor)helmet;

            Health += helmet.HealthBuff;
            Attack += helmet.AttackBuff;
            Mana += helmet.ManaBuff;

            return null;
        }
    }

    private Item? PickUpChestPiece(Item chestPiece)
    {
        // If the hero already has a chest piece, change it to new one
        if (ChestArmor != null)
        {
            // Removing old armor buffs and unequipping old armor
            Health -= ChestArmor.HealthBuff;
            Attack -= ChestArmor.AttackBuff;
            Mana -= ChestArmor.ManaBuff;
            var oldArmor = ChestArmor;

            // Adding new armor buffs and equiping the new armor
            Health += chestPiece.HealthBuff;
            Attack += chestPiece.AttackBuff;
            Mana += chestPiece.ManaBuff;
            ChestArmor = (Armor)chestPiece;

            return oldArmor;
        }
        else
        {
            // Adding new armor buffs and equiping the new armor
            ChestArmor = (Armor)chestPiece;

            Health += chestPiece.HealthBuff;
            Attack += chestPiece.AttackBuff;
            Mana += chestPiece.ManaBuff;

            return null;
        }
    }

    private Item? PickUpArmsArmor(Item armsArmor)
    {
        // If the hero already has arms armor, change it to new one
        if (ArmsArmor != null)
        {
            // Removing old armor buffs and unequipping old armor
            Health -= ArmsArmor.HealthBuff;
            Attack -= ArmsArmor.AttackBuff;
            Mana -= ArmsArmor.ManaBuff;
            var oldArmor = ArmsArmor;

            // Adding new armor buffs and equiping the new armor
            Health += armsArmor.HealthBuff;
            Attack += armsArmor.AttackBuff;
            Mana += armsArmor.ManaBuff;
            ArmsArmor = (Armor)armsArmor;

            return oldArmor;
        }
        else
        {
            // Adding new armor buffs and equiping the new armor
            ArmsArmor = (Armor)armsArmor;

            Health += armsArmor.HealthBuff;
            Attack += armsArmor.AttackBuff;
            Mana += armsArmor.ManaBuff;

            return null;
        }
    }

    private Item? PickUpLegsArmor(Item legsArmor)
    {
        // If the hero already has legs armor, change it to new one
        if (LegsArmor != null)
        {
            // Removing old armor buffs and unequipping old armor
            Health -= LegsArmor.HealthBuff;
            Attack -= LegsArmor.AttackBuff;
            Mana -= LegsArmor.ManaBuff;
            var oldArmor = LegsArmor;

            // Adding new armor buffs and equiping the new armor
            Health += legsArmor.HealthBuff;
            Attack += legsArmor.AttackBuff;
            Mana += legsArmor.ManaBuff;
            LegsArmor = (Armor)legsArmor;

            return oldArmor;
        }
        else
        {
            // Adding new armor buffs and equiping the new armor
            LegsArmor = (Armor)legsArmor;

            Health += legsArmor.HealthBuff;
            Attack += legsArmor.AttackBuff;
            Mana += legsArmor.ManaBuff;

            return null;
        }
    }
}