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
        private Player player;

        [TestInitialize] 
        public void init()
        {
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
            Assert.AreEqual(105, player.Energy);
        }

        [TestMethod]
        public void Player_UseItem()
        {
            // Arrange
            var sword = new Sword();
            var food = new Food();
            player.Inventory.Add(sword);

            // Act
            player.UseTopItem();

            // Assert
            Assert.AreNotEqual(sword, player.Inventory.First(), "Sword should be replaced by Food after use.");
            Assert.IsTrue(player.Inventory.First() is Food);

        }


        [TestMethod]
        public void Player_PrintInventory_IsNotEmpty()
        {
            // Arrange
   
            player.Inventory.Add(new Food());
            player.Inventory.Add(new Sword());

            string expectedOutput =
@"All Items (top to bottom):
 -  Food
 -  Sword";


            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            player.PrintPlayerInventory();

            // Assert
            string actualOutput = sw.ToString().Trim(); 
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]
        public void Player_PrintInvenotry_IsEmpty()
        {
            // Arrage 

            string expectedOutput = "Inventory is Empty";

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act 
            player.PrintPlayerInventory();

            // Assert
            string actualOutput = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

    }
}