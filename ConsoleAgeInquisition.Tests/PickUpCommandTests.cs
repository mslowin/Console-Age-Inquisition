using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(8)]
    public class PickUpCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
        public void PickUpCommand_ShouldPickUpItemWhenItemExists()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var weapon = new Weapon { Name = "Sword" };
            var room = new Room { Hero = hero, ItemsOnTheFloor = new List<Item> { weapon } };
            game.Dungeon = new Dungeon { Rooms = new List<Room> { room } };

            var pickUpCommand = new PickUpCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            pickUpCommand.Execute(["Sword"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.DoesNotContain(room.ItemsOnTheFloor, i => i.Name == "Sword");
            Assert.NotNull(hero.Weapon);
            Assert.Equal("Sword", hero.Weapon.Name);
            Assert.Contains("You picked up Sword!", output);
        }

        [Fact, Order(2)]
        public void PickUpCommand_ShouldPrintErrorWhenItemDoesNotExist()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero };
            game.Dungeon = new Dungeon { Rooms = new List<Room> { room } };

            var pickUpCommand = new PickUpCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            pickUpCommand.Execute(["NonExistentItem"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("No such item to pick up.", output);
        }

        [Fact, Order(3)]
        public void PickUpCommand_ShouldPrintErrorWhenNoItemSpecified()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var room = new Room { Hero = hero };
            game.Dungeon = new Dungeon { Rooms = new List<Room> { room } };

            var pickUpCommand = new PickUpCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            pickUpCommand.Execute([]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.Contains("Specify the item to pick up.", output);
        }

        [Fact, Order(4)]
        public void PickUpCommand_ShouldUnequipItemWhenHeroHasFullInventory()
        {
            // Arrange
            var game = new Game();
            var hero = new Hero();
            var newWeapon = new Weapon { Name = "NewSword" };
            var oldWeapon = new Weapon { Name = "OldSword" };
            hero.Weapon = oldWeapon;
            var room = new Room { Hero = hero, ItemsOnTheFloor = new List<Item> { newWeapon } };
            game.Dungeon = new Dungeon { Rooms = new List<Room> { room } };

            var pickUpCommand = new PickUpCommand(game.Dungeon);

            // Capture Console Output
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            pickUpCommand.Execute(["NewSword"]);

            // Get the console output
            var output = consoleOutput.ToString();

            // Assert
            Assert.NotNull(hero.Weapon);
            Assert.Equal("NewSword", hero.Weapon.Name);
            Assert.Contains(room.ItemsOnTheFloor, i => i.Name == "OldSword");
            Assert.Contains("You picked up NewSword!", output);
            Assert.Contains("You unequipped OldSword!", output);
        }
    }
}
