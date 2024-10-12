using ConsoleAgeInquisition.Models;
using System;
using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Commands;

public class UseCommand : ICommand
{
    private readonly Dungeon _dungeon;

    public UseCommand(Dungeon dungeon)
    {
        this._dungeon = dungeon;
    }

    public void Execute(string[] args)
    {
        var hero = _dungeon.Rooms.Find(room => room.Hero != null)!.Hero;

        if (args.Length == 0)
        {
            Console.WriteLine("Specify the object to use (food or potion).");
            return;
        }

        var objectName = args[0];

        var objectToBeUsed = hero!.Items.Find(e => e.Name == objectName);

        if (objectToBeUsed == null)
        {
            Console.WriteLine("There is no object with that name in your inventory.");
            return;
        }

        if (objectToBeUsed.Type != ItemType.Food && objectToBeUsed.Type != ItemType.Potion)
        {
            Console.WriteLine("You can use only potions or food.");
            return;
        }

        switch (objectToBeUsed.Type)
        {
            case ItemType.Food:
                Console.WriteLine($"You eat {objectToBeUsed.Name}. Your stats: HP: {hero.Health}, ATT: {hero.Attack}, MANA: {hero.Mana}");
                break;
            case ItemType.Potion:
                Console.WriteLine($"You drink {objectToBeUsed.Name}. Your stats: HP: {hero.Health}, ATT: {hero.Attack}, MANA: {hero.Mana}");
                break;
        }

        hero.UseObject(objectToBeUsed);
    }
}