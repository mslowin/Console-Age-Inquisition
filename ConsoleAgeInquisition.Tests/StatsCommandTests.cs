using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(10)]
    public class StatsCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void StatsCommand_ShouldPrintHeroStats()
        {
            // Arrange
            var hero = new Hero
            {
                Name = "TestHero",
                Health = 100,
                Attack = 50,
                Mana = 30,
                Weapon = new Weapon { Name = "Sword", AttackBuff = 10 },
                HeadArmor = new Armor { Name = "Helmet", HealthBuff = 5, ArmorType = ArmorType.Helmet },
                ChestArmor = new Armor { Name = "Chestplate", HealthBuff = 10, ArmorType = ArmorType.ChestPiece },
                ArmsArmor = new Armor { Name = "Gauntlets", HealthBuff = 3, ArmorType = ArmorType.ArmsArmor },
                LegsArmor = new Armor { Name = "Leggings", HealthBuff = 7, ArmorType = ArmorType.LegsArmor },
                Items = new List<Item>
                {
                    new Item { Name = "Potion", Type = ItemType.Potion, HealthBuff = 20 },
                    new Item { Name = "Ring", Type = ItemType.PowerRing, ManaBuff = 10 }
                }
            };
            var dungeon = new Dungeon
            {
                Rooms = new List<Room> { new Room { Hero = hero } }
            };

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var statsCommand = new StatsCommand(dungeon);
            statsCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("- Name: TestHero (HP: 100, ATT: 50, MANA: 30)", output);
            Assert.Contains("* Weapon:", output);
            Assert.Contains("+ Sword (ATT buff: 10)", output);
            Assert.Contains("* Armor:", output);
            Assert.Contains("+ Head armor: Helmet (HP buff: 5)", output);
            Assert.Contains("+ Chest armor: Chestplate (HP buff: 10)", output);
            Assert.Contains("+ Arms armor: Gauntlets (HP buff: 3)", output);
            Assert.Contains("+ Legs armor: Leggings (HP buff: 7)", output);
            Assert.Contains("* Items:", output);
            Assert.Contains("+ Potion (Type: Potion HP buff: 20, ATT buff: 0, MANA buff: 0)", output);
            Assert.Contains("+ Ring (Type: PowerRing HP buff: 0, ATT buff: 0, MANA buff: 10)", output);
        }

        [Fact, Order(2)]
        public void StatsCommand_ShouldPrintNoWeaponWhenNoWeaponEquipped()
        {
            // Arrange
            var hero = new Hero
            {
                Name = "TestHero",
                Health = 100,
                Attack = 50,
                Mana = 30
            };
            var dungeon = new Dungeon
            {
                Rooms = new List<Room> { new Room { Hero = hero } }
            };

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var statsCommand = new StatsCommand(dungeon);
            statsCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("* No weapon equipped.", output);
        }

        [Fact, Order(3)]
        public void StatsCommand_ShouldPrintNoArmorWhenNoArmorEquipped()
        {
            // Arrange
            var hero = new Hero
            {
                Name = "TestHero",
                Health = 100,
                Attack = 50,
                Mana = 30
            };
            var dungeon = new Dungeon
            {
                Rooms = new List<Room> { new Room { Hero = hero } }
            };

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var statsCommand = new StatsCommand(dungeon);
            statsCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("* No armor equipped.", output);
        }

        [Fact, Order(4)]
        public void StatsCommand_ShouldPrintNoItemsWhenNoItemsInBackpack()
        {
            // Arrange
            var hero = new Hero
            {
                Name = "TestHero",
                Health = 100,
                Attack = 50,
                Mana = 30
            };
            var dungeon = new Dungeon
            {
                Rooms = new List<Room> { new Room { Hero = hero } }
            };

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var statsCommand = new StatsCommand(dungeon);
            statsCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("* No items in the backpack.", output);
        }

        [Fact, Order(5)]
        public void StatsCommand_ShouldPrintErrorWhenHeroNotFound()
        {
            // Arrange
            var dungeon = new Dungeon
            {
                Rooms = new List<Room> { new Room { Hero = null } }
            };

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var statsCommand = new StatsCommand(dungeon);
            statsCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Something went wrong. Try again.", output);
        }
    }
}
