using ConsoleAgeInquisition.Services;
using ConsoleAgeInquisition.Models;
using System.Reflection;

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

            // Get the number of commands by getting the number of files in Commands folder
            var test = GetSolutionPath();
            var solutionDirectory = GetSolutionPath();

            var commandsFolderPath = Path.Combine(solutionDirectory, "Commands");

            var commandsCount = Directory.GetFiles(commandsFolderPath)
                .Count(file => !file.Contains("ICommand"));

            var commandFilesNames = Directory.GetFiles(commandsFolderPath)
                .Where(file => !file.Contains("ICommand"))
                .Select(file => Path.GetFileNameWithoutExtension(file))
                .ToList();

            var commandNames = commandFilesNames.Select(name => name.ToLower().Replace("command", "")).ToList();

            // Act
            GameService.Initialize(commandService, game);

            // Assert
            for (int i = 0; i < commandsCount; i++)
            {
                // checking if all commands are registered
                Assert.True(commandService.IsCommandRegistered(commandNames[i]));
            }
        }

        [Fact]
        public void Run_ShouldEndGameWhenHeroPicksUpDiamonds()
        {
            // Arrange
            var hero = new Hero();
            hero.Items = [Resources.GetDiamondOre()];
            
            var game = new Game();
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            GameService.Run(game);

            // Assert
            // Get the console output
            var output = consoleOutput.ToString();

            // Get the introduction message and victory message that should have been printed
            consoleOutput.GetStringBuilder().Clear();
            ViewsService.IntroducePlayer();
            ViewsService.VictoryMessage();
            var victoryMessage = consoleOutput.ToString();

            Assert.Equal(victoryMessage, output);
        }

        [Fact]
        public void Run_ShouldEndGameWhenHeroDies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 0 };
            game.Dungeon.Rooms.Add(new Room { Hero = hero });

            // Act
            // Mock somehow ViewsService and Console.ReadLine or sth else
            // Assert
        }

        public static string? GetAssemblyDir() => Path.GetDirectoryName(GetAssemblyPath());
        public static string GetAssemblyPath() => Assembly.GetExecutingAssembly().Location;

        private static string GetSolutionPath()
        {
            var currentDirPath = GetAssemblyDir();
            while (currentDirPath != null)
            {
                var fileInCurrentDir = Directory.GetFiles(currentDirPath).Select(f => f.Split(@"\").Last()).ToArray();
                var solutionFileName = fileInCurrentDir.SingleOrDefault(f => f.EndsWith(".sln", StringComparison.InvariantCultureIgnoreCase));
                if (solutionFileName != null)
                    return Path.Combine(currentDirPath, solutionFileName.Replace(".sln", ""));

                currentDirPath = Directory.GetParent(currentDirPath)?.FullName;
            }

            throw new FileNotFoundException("Cannot find solution file path");
        }
    }
}