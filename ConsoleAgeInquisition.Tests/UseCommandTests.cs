using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(11)]
    public class UseCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void UseCommand_ShouldPrintErrorWhenNoArgsProvided()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { new Room { Hero = hero } };

            var useCommand = new UseCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            useCommand.Execute(Array.Empty<string>());

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Specify the object to use (food or potion).", output);
        }

        [Fact, Order(2)]
        public void UseCommand_ShouldPrintErrorWhenObjectNotFound()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { new Room { Hero = hero } };

            var useCommand = new UseCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            useCommand.Execute(new string[] { "NonExistentObject" });

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("There is no object with that name in your inventory.", output);
        }

        [Fact, Order(3)]
        public void UseCommand_ShouldPrintErrorWhenObjectIsNotUsable()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var item = new Item { Name = "NonUsableItem", Type = ItemType.Weapon };
            hero.Items.Add(item);
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { new Room { Hero = hero } };

            var useCommand = new UseCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            useCommand.Execute(new string[] { "NonUsableItem" });

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("You can use only potions or food.", output);
        }

        [Fact, Order(4)]
        public void UseCommand_ShouldPrintStatsWhenUsingFood()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 100, Attack = 50, Mana = 30 };
            var food = new Item { Name = "Apple", Type = ItemType.Food };
            hero.Items.Add(food);
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { new Room { Hero = hero } };

            var useCommand = new UseCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            useCommand.Execute(new string[] { "Apple" });

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("You eat Apple. Your stats: HP: 100, ATT: 50, MANA: 30", output);
        }

        [Fact, Order(5)]
        public void UseCommand_ShouldPrintStatsWhenUsingPotion()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 100, Attack = 50, Mana = 30 };
            var potion = new Item { Name = "HealthPotion", Type = ItemType.Potion };
            hero.Items.Add(potion);
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { new Room { Hero = hero } };

            var useCommand = new UseCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            useCommand.Execute(new string[] { "HealthPotion" });

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("You drink HealthPotion. Your stats: HP: 100, ATT: 50, MANA: 30", output);
        }
    }
}
