using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(7)]
    public class OpenCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void OpenCommand_ShouldOpenChestWhenNoEnemies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var chest = new Chest { Name = "Chest1", Items = new List<Item> { new Item { Name = "Item1" } } };
            var room = new Room { Hero = hero, Chests = new List<Chest> { chest }, Enemies = new List<Enemy>() };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { room };

            var openCommand = new OpenCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            openCommand.Execute(["Chest1"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Chest has been opened and the items were thrown on the ground. Use \"look\" command", output);
            Assert.Contains(room.ItemsOnTheFloor, item => item.Name == "Item1");
        }

        [Fact, Order(2)]
        public void OpenCommand_ShouldNotOpenChestWhenEnemiesPresent()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var chest = new Chest { Name = "Chest1" };
            var enemy = new Enemy();
            var room = new Room { Hero = hero, Chests = new List<Chest> { chest }, Enemies = new List<Enemy> { enemy } };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { room };

            var openCommand = new OpenCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            openCommand.Execute(["Chest1"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("You can't open chests when there are enemies nearby.", output);
            Assert.DoesNotContain(room.ItemsOnTheFloor, item => item.Name == "Item1");
        }

        [Fact, Order(3)]
        public void OpenCommand_ShouldPrintErrorWhenChestNotFound()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, Chests = new List<Chest>(), Enemies = new List<Enemy>() };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { room };

            var openCommand = new OpenCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            openCommand.Execute(["NonExistentChest"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("There is no chest with given name in this room.", output);
        }

        [Fact, Order(4)]
        public void OpenCommand_ShouldPrintErrorWhenNoChestSpecified()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, Chests = new List<Chest>(), Enemies = new List<Enemy>() };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { room };

            var openCommand = new OpenCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            openCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Specify the chest to open.", output);
        }
    }
}
