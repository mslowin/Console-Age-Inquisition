namespace ConsoleAgeInquisition.Tests
{
    public class GoCommandTests
    {
        [Fact]
        public void GoCommand_ShouldMoveHeroToNextRoom()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var currentRoom = new Room { Hero = hero, EastDoorId = 1 };
            var nextRoom = new Room { WestDoorId = 1 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { currentRoom, nextRoom };

            var goCommand = new GoCommand(game.Dungeon);

            // Act
            goCommand.Execute(["east"]);

            // Assert
            Assert.Null(currentRoom.Hero);
            Assert.Equal(hero, nextRoom.Hero);
        }

        [Fact]
        public void GoCommand_ShouldNotMoveHeroIfEnemiesPresent()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var enemy = new Enemy();
            var currentRoom = new Room { Hero = hero, Enemies = new List<Enemy> { enemy } };
            var nextRoom = new Room();
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { currentRoom, nextRoom };

            var goCommand = new GoCommand(game.Dungeon);

            // Capture Console Output
            var originalConsoleOut = Console.Out;
            try
            {
                using var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput);

                // Act
                goCommand.Execute(new string[] { "east" });

                // Get the console output
                var output = consoleOutput.ToString();

                // Assert
                Assert.Contains("You can't go to another room if there are enemies around.", output);
                Assert.Equal(hero, currentRoom.Hero);
                Assert.Null(nextRoom.Hero);
            }
            finally
            {
                // Restore the original console output
                Console.SetOut(originalConsoleOut);
            }
        }

        [Fact]
        public void GoCommand_ShouldPrintErrorWhenNoDirectionSpecified()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var currentRoom = new Room { Hero = hero };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { currentRoom };

            var goCommand = new GoCommand(game.Dungeon);

            // Capture Console Output
            var originalConsoleOut = Console.Out;
            try
            {
                using var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput);

                // Act
                goCommand.Execute(new string[] { });

                // Get the console output
                var output = consoleOutput.ToString();

                // Assert
                Assert.Contains("Specify the direction.", output);
                Assert.Equal(hero, currentRoom.Hero);
            }
            finally
            {
                // Restore the original console output
                Console.SetOut(originalConsoleOut);
            }
        }

        [Fact]
        public void GoCommand_ShouldPrintErrorWhenNoDoorInSpecifiedDirection()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var currentRoom = new Room { Hero = hero };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = new List<Room> { currentRoom };

            var goCommand = new GoCommand(game.Dungeon);

            // Capture Console Output
            var originalConsoleOut = Console.Out;
            try
            {
                using var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput);

                // Act
                goCommand.Execute(new string[] { "north" });

                // Get the console output
                var output = consoleOutput.ToString();

                // Assert
                Assert.Contains("There is no door to the north.", output);
                Assert.Equal(hero, currentRoom.Hero);
            }
            finally
            {
                // Restore the original console output
                Console.SetOut(originalConsoleOut);
            }
        }
    }
}