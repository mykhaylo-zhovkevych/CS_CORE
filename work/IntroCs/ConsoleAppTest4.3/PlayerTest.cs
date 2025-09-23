using ConsoleApp4._3;
using ConsoleApp4._3.Fields;
using ConsoleApp4._3.Interfaces;
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
    public class PlayerTest
    {

        private Bag bag;
        private Food food;
        private Key key;
        private Sword sword;
        private Player player;

        [TestInitialize] 
        public void init()
        {
            bag = new Bag();
            food = new Food();
            key = new Key();
            sword = new Sword();
            player = new Player("Jonny", 100);
        }


        [TestMethod]
        public void Player_ConsumeItem()
        {

            // Arrange
            var food = new Food();
            player.Inventory.Add(food);

            // Act
            player.UseTopItem();

            // Assert
            Assert.IsTrue(player.Energy > 100, "Player energy must increase after consuming food.");
        }

        [TestMethod]
        public void Player_UseItem()
        {
            // Arrange
            var sword = new Sword();
            player.Inventory.Add(sword);

            // Act
            player.UseTopItem();

            // Assert
            Assert.AreNotEqual(sword, player.Inventory.First(), "Sword should be replaced by Food after use.");

        }

        [TestMethod]
        public void Player_PrintInventory()
        {
            // Arrange
            var player = new Player("Hero", 200);
            player.Inventory.Add(new Food());
            player.Inventory.Add(new Sword());

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            player.PrintPlayerInventory();

            var output = stringWriter.ToString();

            // Assert
            StringAssert.Contains(output, "All Items (top to bottom):");
            StringAssert.Contains(output, "Food");
            StringAssert.Contains(output, "Sword");

        }
    }
}