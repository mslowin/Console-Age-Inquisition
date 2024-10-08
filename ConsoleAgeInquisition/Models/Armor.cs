﻿using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Models;

public class Armor : Item
{
    public ArmorType ArmorType;

    public Armor()
    {
        Type = ItemType.Armor;
    }
}