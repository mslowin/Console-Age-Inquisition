using ConsoleAgeInquisition.Services;
using ConsoleAgeInquisition.Models;

namespace ConsoleAgeInquisition.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Initialize_ShouldRegisterAllCommands()
        {
            // Arrange
            var commandService = new CommandService();
            var game = new Game();

            // Act
            GameService.Initialize(commandService, game);

            // Assert
            Assert.True(commandService.IsCommandRegistered("look"));
            Assert.True(commandService.IsCommandRegistered("stats"));
            Assert.True(commandService.IsCommandRegistered("attack"));
            Assert.True(commandService.IsCommandRegistered("use"));
            Assert.True(commandService.IsCommandRegistered("go"));
            Assert.True(commandService.IsCommandRegistered("open"));
            Assert.True(commandService.IsCommandRegistered("pickup"));
            Assert.True(commandService.IsCommandRegistered("examine"));
            Assert.True(commandService.IsCommandRegistered("save"));
            Assert.True(commandService.IsCommandRegistered("restart"));
            Assert.True(commandService.IsCommandRegistered("exit"));
            Assert.True(commandService.IsCommandRegistered("help"));
        }

        [Fact]
        public void Run_ShouldEndGameWhenHeroPicksUpDiamonds()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            hero.Items.Add(Resources.GetDiamondOre());
            game.Dungeon.Rooms.Add(new Room { Hero = hero });

            // Act
            // Mock somehow ViewsService and Console.ReadLine

            // Assert
        }

        [Fact]
        public void Run_ShouldEndGameWhenHeroDies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 0 };
            game.Dungeon.Rooms.Add(new Room { Hero = hero });

            // Act
            // Mock somehow ViewsService and Console.ReadLine
            // Assert
        }
    }
}