using ConsoleApp4._3;
using ConsoleApp4._3.Exceptions;
using ConsoleApp4._3.Fields;
using ConsoleApp4._3.Items;

namespace ConsoleAppTest4._3
{
    [TestClass]
    public sealed class UnitTest
    {
        // Test01
        // Move() Player can correct move with correct coordinate like (North South West East)
        // Test02
        // Check if Player can access of bounds, (should run Console.WriteLine and set player at (0, 0))
        // Test03
        // Can Player access the forbitten field, and if yes which conditions are needed 

        // Test04
        // Needs: Player Object, Item Object, Field Object
        // Statement: Player can interact with Objects (Items) Pick Up, Drop
        // Player can pick up/ drop etc... an item from the field (If there Items is)

        PlayField game;
        
        [TestInitialize]
        public void Initialize()
        {
            game = new PlayField("Quest", new KeyboardController());
            game.Player.Inventory.Add(new Key());
            game.Fields[(0, 2)] = new Wall("Wall");
            game.Fields[(1, 1)] = new Door("Door", (4, 4));

        }
        [TestMethod]
        public void GameMovePlayerToSpecificField()
        {
            // Act 
            game.MovePlayer(Direction.East);

            // Assert
            Assert.AreEqual((1, 0), game.Player.Position);
        }
        [TestMethod]
        public void GameMovePlayerOutOfBoundaries()
        {
            // Arrage
            game.Player.Position = (6, 0);

            var oldPos = game.Player.Position;

            // Act 
            game.MovePlayer(Direction.East);

            // Assert
            var newPos = game.Player.Position;
            Assert.AreEqual(oldPos, newPos, "Player should not move outside the boundaries. ");

        }
        [TestMethod] 
        public void GameMovePlayerIntoNonAccesibleField()
        {
            // Arrage
            game.Player.Position = (0, 1);

            // Act
            game.MovePlayer(Direction.South);
            var afterWall = game.Player.Position;

            // Through the Door

            game.MovePlayer(Direction.East);
            var afterDoor = game.Player.Position;

            // Assert
            Assert.AreEqual((0, 1), afterWall, "Player should not move into a wall.");

            Assert.AreEqual((4, 4), afterDoor, "Player should had been teleported through the door after unlocking it.");
        }
        [TestMethod]
        public void PlayerPickUpItemIntoInventory()
        {
            // Arrange
            var startPos = game.Player.Position; 
            var field = game.Fields[startPos];
            var item = new Food();
            field.Items.Add(item);

            int initialInventoryCount = game.Player.Inventory.Count;
            int initialFieldItemCount = field.Items.Count;

            // Act
            game.PickUpItem();

            // Assert
            Assert.AreEqual(initialInventoryCount + 1, game.Player.Inventory.Count);
            Assert.AreEqual(initialFieldItemCount - 1, field.Items.Count);
            Assert.IsTrue(game.Player.Inventory.Contains(item), "Player inventory must contain picked up item");
            Assert.IsFalse(field.Items.Contains(item), "Field must no longer contain the picked item");
        }
        [TestMethod]
        public void PlayerDropDownItemFromInventory()
        {
            // Arrange

            var startPos = game.Player.Position;
            var field = game.Fields[startPos];
            var item = new Food();
            game.Player.Inventory.Add(item);

            int initialInventoryCount = game.Player.Inventory.Count;
            int initialFieldItemCount = field.Items.Count;

            // Act
            game.DropItem();

            // Assert
            Assert.AreEqual(initialInventoryCount - 1, game.Player.Inventory.Count);
            Assert.AreEqual(initialFieldItemCount + 1, field.Items.Count);
            Assert.IsFalse(game.Player.Inventory.Contains(item));
            Assert.IsTrue(field.Items.Contains(item));
        }
    }
}
