using Moq;
using XUnitPriorityOrderer;

namespace ConsoleAgeInquisition.Tests
{
    [Order(1)]
    public class AttackCommandTests : BaseTestsClass
    {
        [Fact, Order(1)]
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

        [Fact, Order(2)]
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

        [Fact, Order(3)]
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

        [Fact, Order(4)]
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

        [Fact, Order(5)]
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

        [Fact, Order(6)]
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

        [Fact, Order(7)]
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

        [Fact, Order(8)]
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
    }
}
