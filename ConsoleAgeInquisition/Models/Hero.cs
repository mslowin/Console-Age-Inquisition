using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Hero : Character
{
    public void PickUpItem(Item item)
    {
        if (item.Type == ItemType.Weapon)
        {
            // If the hero already has a weapon, change it to new one
            if (Weapon != null)
            {
                // Removing old weapon buffs
                Health -= Weapon.HealthBuff;
                Attack -= Weapon.AttackBuff;
                Mana -= Weapon.ManaBuff;

                Weapon = (Weapon)item;
            }
        }

        if (item.Type == ItemType.Armor)
        {
            // TODO: w zależności jaki to typ armoru to zamieniamy konkretną część
            // I też te buffy trzeba zabrać od starego armoru
        }

        Health += item.HealthBuff;
        Attack += item.AttackBuff;
        Mana += item.ManaBuff;
    }
}