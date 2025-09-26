using ConsoleApp4._3;
using ConsoleApp4._3.Fields;
using ConsoleApp4._3.Items;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest4._3
{

    [TestClass]
    public class FieldTest
    {

        private Door door;
        private Enemy enemy;
        private Player player;

        [TestInitialize]
        public void Init()
        {
          
            door = new Door("Door");
            enemy = new Enemy("Spider");
            player = new Player("Hero", default);

        }

        [TestMethod]
        public void Wall_MustBlockEntering()
        {
            // Arrange
        
            Wall wall = new Wall("Wall");

            // Act
            // better naming 
            var result = wall.MovePlayerToField(player);

            // Assert 
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Enter_Door_Without_Key_Should_Blocked()
        {
            // Arrange

            // Act
            var result = door.MovePlayerToField(player);

            // Assert 
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Enter_Door_With_Key_Should_BeAllowed()
        {
            // Arrange
            var key = new Key();
            player.Inventory.Add(key);
            // var door = new TestDoor();

            // Act
            var result = door.MovePlayerToField(player);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Door_MustAllowEntering_IfKey()
        {
            // Arrange 
            var key = new Key();
            player.Inventory.Add(key);
            // how is this called? 
            Door fakeDoor = new Door("Door");

            // Act
            var result = fakeDoor.MovePlayerToField(player);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Door_MustBlockEntering_IfNoKey()
        {
            // Arrange
            Door fakeDoor = new Door("Door");

            // Act 
            var result = fakeDoor.MovePlayerToField(player);

            // Assert 
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void Grass_CanEnter_MustAllowEntering()
        {
            // Arrange
            var field = new Grass("Grass");

            // Act

            var passed = field.MovePlayerToField(player);

            // Assert
            Assert.IsTrue(passed);
        }

        [TestMethod] 
        public void Enemy_MustDamangePlayer_IfNoSword()
        {
            // Arrange

            var initialEnergy = 100;
            player = new Player("Hero", initialEnergy);

            // Act

            enemy.MovePlayerToField(player);

            // Assert

            Assert.AreEqual(90, player.Energy, "The enemy must harm the player if no sword is present.");

        }

        [TestMethod]
        public void Enemy_MustNotDamangePlayer_IfSword()
        {

            // Arrange

            var initialEnergy = 100;
            player = new Player("Hero", initialEnergy);
            var sword = new Sword();

            player.Inventory.Add(sword);    

            // Act 

            enemy.MovePlayerToField(player);

            // Assert
            Assert.AreEqual(initialEnergy, player.Energy, "The enemy must not harm the player as long as a sword is present.");
        }
    }
}
