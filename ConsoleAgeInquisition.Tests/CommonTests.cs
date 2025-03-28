using ConsoleAgeInquisition.Services;
using System.Reflection;
using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(2)]
    public class CommonTests : BaseTestsClass
    {
        [Fact, Order(1)]
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

        [Fact, Order(2)]
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

            // Get the console output
            var output = consoleOutput.ToString();

            // Get the introduction message and victory message that should have been printed
            consoleOutput.GetStringBuilder().Clear();
            ViewsService.IntroducePlayer();
            ViewsService.VictoryMessage();
            var victoryMessage = consoleOutput.ToString();

            // Assert
            Assert.Equal(victoryMessage, output);
        }

        [Fact, Order(3)]
        public void Run_ShouldEndGameWhenHeroDies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 0 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero }];

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            GameService.Run(game);

            // Get the console output
            var output = consoleOutput.ToString();

            // Get the introduction message and victory message that should have been printed
            consoleOutput.GetStringBuilder().Clear();
            ViewsService.IntroducePlayer();
            ViewsService.YouDiedMessage();
            var victoryMessage = consoleOutput.ToString();

            // Assert
            Assert.Equal(victoryMessage, output);
        }

        private static string? GetAssemblyDir() => Path.GetDirectoryName(GetAssemblyPath());

        private static string GetAssemblyPath() => Assembly.GetExecutingAssembly().Location;

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

        [Fact, Order(4)]
        public void Run_ShouldCompleteGameWithAllActions()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Health = 100, Attack = 50, Mana = 30 };
            var enemy1 = new Enemy { Name = "Enemy1", Health = 50, Attack = 10 };
            var enemy2 = new Enemy { Name = "Enemy2", Health = 50, Attack = 10 };
            var boss = new Enemy { Name = "Boss", Health = 100, Attack = 20 };
            var weapon = new Weapon { Name = "Sword", AttackBuff = 20 };
            var potion = new Item { Name = "HealthPotion", Type = ItemType.Potion, HealthBuff = 50 };
            var diamond = Resources.GetDiamondOre();

            var room1 = new Room { Hero = hero, Enemies = new List<Enemy> { enemy1 }, ItemsOnTheFloor = new List<Item> { weapon }, NorthDoorId = 0};
            var room2 = new Room { Enemies = new List<Enemy> { enemy2 }, ItemsOnTheFloor = new List<Item> { potion }, SouthDoorId = 0, NorthDoorId = 1};
            var room3 = new Room { Enemies = new List<Enemy> { boss }, ItemsOnTheFloor = new List<Item> { diamond }, SouthDoorId = 1 };

            game.Dungeon = new Dungeon { Rooms = new List<Room> { room1, room2, room3 } };

            var commandService = new CommandService();
            GameService.Initialize(commandService, game);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            // Hero picks up the weapon
            var pickUpCommand = new PickUpCommand(game.Dungeon);
            pickUpCommand.Execute(new string[] { "Sword" });

            // Hero attacks and kills the first enemy
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute(new string[] { "Enemy1" });

            // Hero moves to the next room
            var goCommand = new GoCommand(game.Dungeon);
            goCommand.Execute(new string[] { "north" });

            // Hero picks up the potion
            pickUpCommand.Execute(new string[] { "HealthPotion" });

            // Hero uses the potion
            var useCommand = new UseCommand(game.Dungeon);
            useCommand.Execute(new string[] { "HealthPotion" });

            // Hero attacks and kills the second enemy
            attackCommand.Execute(new string[] { "Enemy2" });

            // Hero moves to the final room
            goCommand.Execute(new string[] { "north" });

            // Hero attacks and kills the boss
            attackCommand.Execute(new string[] { "Boss" });

            // Hero picks up the diamond
            pickUpCommand.Execute(new string[] { diamond.Name });

            // Run the game to check for victory
            GameService.Run(game);

            // Get the console output
            var output = consoleOutput.ToString();

            // Get the introduction message and victory message that should have been printed
            consoleOutput.GetStringBuilder().Clear();
            ViewsService.VictoryMessage();
            var victoryMessage = consoleOutput.ToString();

            // Assert
            Assert.Contains(victoryMessage, output);
        }
    }
}