using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(6)]
    public class LookCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void LookCommand_ShouldPrintRoomDescription()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { RoomName = "TestRoom", Hero = hero };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [room];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var lookCommand = new LookCommand(game.Dungeon);
            lookCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains(room.RoomName, output);
        }

        [Fact, Order(2)]
        public void LookCommand_ShouldPrintRoomItems()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, ItemsOnTheFloor = [new Item { Name = "Item1" }, new Item { Name = "Item2" }] };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [room];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var lookCommand = new LookCommand(game.Dungeon);
            lookCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Item1", output);
            Assert.Contains("Item2", output);
        }

        [Fact, Order(3)]
        public void LookCommand_ShouldPrintRoomEnemies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, Enemies = [new Enemy { Name = "Enemy1" }, new Enemy { Name = "Enemy2" }] };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [room];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var lookCommand = new LookCommand(game.Dungeon);
            lookCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Enemy1", output);
            Assert.Contains("Enemy2", output);
        }

        [Fact, Order(4)]
        public void LookCommand_ShouldPrintRoomChests()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, Chests = [new Chest { Name = "Chest1" }, new Chest { Name = "Chest2" }] };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [room];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var lookCommand = new LookCommand(game.Dungeon);
            lookCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Chest1", output);
            Assert.Contains("Chest2", output);
        }

        [Fact, Order(5)]
        public void LookCommand_ShouldPrintRoomDoors()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero, NorthDoorId = 1, SouthDoorId = 2, EastDoorId = 3, WestDoorId = 4 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [room];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var lookCommand = new LookCommand(game.Dungeon);
            lookCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("North", output);
            Assert.Contains("South", output);
            Assert.Contains("East", output);
            Assert.Contains("West", output);
        }
    }
}
