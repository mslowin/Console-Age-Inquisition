using ConsoleAgeInquisition.Services;
using Moq;
using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(5)]
    public class HelpCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void HelpCommand_ShouldListAllCommands()
        {
            // Arrange
            var commandService = new CommandService();
            var helpCommand = new HelpCommand(commandService);

            // Register some dummy commands for testing
            commandService.RegisterCommand("attack", new Mock<ICommand>().Object, "Attack an enemy");
            commandService.RegisterCommand("look", new Mock<ICommand>().Object, "Look around the room");

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            helpCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("attack: Attack an enemy", output);
            Assert.Contains("look: Look around the room", output);
        }
    }
}
