using ConsoleAgeInquisition.Services;
using ConsoleAgeInquisition.Models;
using System.Reflection;
using Moq;
using ConsoleAgeInquisition.Commands;
using ConsoleAgeInquisition.Enums;

namespace ConsoleAgeInquisition.Tests
{
    public class CommandsTests
    {

        #region Common

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

        [Fact]
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

        #endregion

        #region AttackCommand

        [Fact]
        public void AttackCommand_ShouldKillEnemyWhenRightAmountOfAttack()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy = new Enemy { Health = 100 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy.Name]);

            // Assert
            Assert.Empty(game.Dungeon.Rooms[0].Enemies);
        }

        [Fact]
        public void AttackCommand_ShouldDropItemsWhenEnemyDies()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy = new Enemy { Health = 100, Items = new List<Item> { new Item { Name = "Item" } } };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy], ItemsOnTheFloor = [] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy.Name]);

            // Assert
            Assert.Empty(game.Dungeon.Rooms[0].Enemies);
            Assert.Contains(game.Dungeon.Rooms[0].ItemsOnTheFloor, item => item.Name == "Item");
        }

        [Fact]
        public void AttackCommand_ShouldNotKillEnemyWhenAttackIsInsufficient()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 50 };
            var enemy = new Enemy { Health = 100 };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy.Name]);

            // Assert
            Assert.Contains(game.Dungeon.Rooms[0].Enemies, e => e.Health == 50);
        }

        [Fact]
        public void AttackCommand_ShouldNotAffectOtherEnemiesWhenAttackingOne()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy1 = new Enemy { Health = 100, Name = "Enemy1" };
            var enemy2 = new Enemy { Health = 100, Name = "Enemy2" };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy1, enemy2] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy1.Name]);

            // Assert
            Assert.Empty(game.Dungeon.Rooms[0].Enemies.Where(e => e.Name == "Enemy1"));
            Assert.Contains(game.Dungeon.Rooms[0].Enemies, e => e.Name == "Enemy2" && e.Health == 100);
        }

        [Fact]
        public void AttackCommand_ShouldNotExecuteWhenEnemyNotFound()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy = new Enemy { Health = 100, Name = "Enemy1" };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute(["NonExistentEnemy"]);

            // Assert
            Assert.Contains(game.Dungeon.Rooms[0].Enemies, e => e.Name == "Enemy1" && e.Health == 100);
        }

        [Fact]
        public void AttackCommand_ShouldDecreaseHeroHealthWhenEnemyCounterAttacks()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 20, Health = 100 };

            var enemyMock = new Mock<Enemy>();
            enemyMock.Setup(e => e.AttackHero()).Returns(20);
            var enemy = enemyMock.Object;
            enemy.Health = 100;

            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy.Name]);

            // Assert
            Assert.Equal(80, hero.Health);
        }

        [Fact]
        public void AttackCommand_ShouldNotAffectEnemiesInOtherRooms()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy1 = new Enemy { Health = 100, Name = "Enemy1" };
            var enemy2 = new Enemy { Health = 100, Name = "Enemy2" };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [
                new Room { Hero = hero, Enemies = [enemy1] },
                new Room { Enemies = [enemy2] }
            ];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([enemy1.Name]);

            // Assert
            Assert.Empty(game.Dungeon.Rooms[0].Enemies);
            Assert.Contains(game.Dungeon.Rooms[1].Enemies, e => e.Name == "Enemy2" && e.Health == 100);
        }

        [Fact]
        public void AttackCommand_ShouldNotExecuteWhenInputIsInvalid()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero { Attack = 100 };
            var enemy = new Enemy { Health = 100, Name = "Enemy1" };
            game.Dungeon = new Dungeon();
            game.Dungeon.Rooms = [new Room { Hero = hero, Enemies = [enemy] }];

            // Act
            var attackCommand = new AttackCommand(game.Dungeon);
            attackCommand.Execute([]);

            // Assert
            Assert.Contains(game.Dungeon.Rooms[0].Enemies, e => e.Name == "Enemy1" && e.Health == 100);
        }

        #endregion

        #region LookCommand

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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

        #endregion

        #region ExamineCommand

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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

        #endregion

        #region GoCommand

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

        #endregion

        #region GoCommand
        #endregion

        #region HelpCommand
        #endregion

        #region OpenCommand
        #endregion

        #region PickUpCommand
        #endregion

        #region RestartCommand
        #endregion

        #region SaveCommand
        #endregion

        #region StatsCommand
        #endregion

        #region UseCommand
        #endregion
    }
}