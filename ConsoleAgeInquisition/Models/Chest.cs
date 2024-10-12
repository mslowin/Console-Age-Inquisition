﻿namespace ConsoleAgeInquisition.Models;

public class Chest
{
    public string Name;

    public List<Item> Items;

    public void Open(Room currentRoom)
    {
        currentRoom.ItemsOnTheFloor.AddRange(Items);
        currentRoom.Chests.Remove(this);
    }
}