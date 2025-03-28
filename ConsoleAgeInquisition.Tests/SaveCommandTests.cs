using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(9)]
    public class SaveCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void SaveCommand_ShouldPrintErrorWhenNoSaveNameProvided()
        {
            // Arrange
            var game = new Game();
            var saveCommand = new SaveCommand(game);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            saveCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Please provide a valid save name.", output);
        }
    }
}
