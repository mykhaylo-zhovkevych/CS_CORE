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

        public class TestConsumable : Item, IConsumable
        {
            public bool WasConsumed { get; private set; } = false;

            public TestConsumable() : base() { }

            public void Consume(Player player)
            {
                WasConsumed = true;
            }
        }

        public class TestUsable : Item, IUsable
        {
            public bool WasUsed { get; private set; } = false;

            public TestUsable() : base() { }

            public void Use(Player player)
            {
                WasUsed = true;
            }
        }


        [TestMethod]
        public void Player_ConsumeItem()
        {

            // Arrange
            var consumable = new TestConsumable();
            //player.Inventory.Add(consumable);
            player.Inventory.Add(new Food());

            // Act
            player.UseTopItem();

            // Assert
            //Assert.IsTrue(consumable.WasConsumed, "The consumable item must be consumed.");
            Assert.IsTrue(player.Energy > 100, "Player energy must increase after consuming food.");
        }

        [TestMethod]
        public void Player_UseItem()
        {
            // Arrange
            var usable = new TestUsable();
            player.Inventory.Add(usable);

            // Act
            player.UseTopItem();

            // Assert
            Assert.IsTrue(usable.WasUsed, "The usable item must be used.");

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