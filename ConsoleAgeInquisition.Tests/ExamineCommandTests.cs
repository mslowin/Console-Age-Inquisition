using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(3)]
    public class ExamineCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void ExamineCommand_ShouldPrintEnemyDetails()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var enemy = new Enemy { Name = "Enemy1", Health = 100, Attack = 50, Mana = 30 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var examineCommand = new ExamineCommand(game.Dungeon);
            examineCommand.Execute([enemy.Name]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Enemy1", output);
            Assert.Contains("HP: 100", output);
            Assert.Contains("ATT: 50", output);
            Assert.Contains("MANA: 30", output);
        }

        [Fact, Order(2)]
        public void ExamineCommand_ShouldPrintItemDetails()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var item = new Item { Name = "Item1", Type = ItemType.Weapon, AttackBuff = 10 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, ItemsOnTheFloor = [item] }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var examineCommand = new ExamineCommand(game.Dungeon);
            examineCommand.Execute([item.Name]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Item1", output);
            Assert.Contains("Type: Weapon", output);
            Assert.Contains("Attack buff: 10", output);
        }

        [Fact, Order(3)]
        public void ExamineCommand_ShouldPrintChestDetails()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var chest = new Chest { Name = "Chest1", Items = new List<Item> { new Item { Name = "Item1", Type = ItemType.Potion } } };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Chests = [chest] }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var examineCommand = new ExamineCommand(game.Dungeon);
            examineCommand.Execute([chest.Name]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Chest1", output);
            Assert.Contains("Item1", output);
            Assert.Contains("Type: Potion", output);
        }

        [Fact, Order(4)]
        public void ExamineCommand_ShouldPrintErrorMessageWhenObjectNotFound()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var examineCommand = new ExamineCommand(game.Dungeon);
            examineCommand.Execute(["NonExistentObject"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("No such object to examine.", output);
        }
    }
}
