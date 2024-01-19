using NUnit.Framework;
using SwinAdventure;

namespace InventoryTests
{
    [TestFixture()]
    internal class TestInventory
    {
        Inventory inventoryTest;
        Item swordTest, pcTest, shovelTest;
        string testList;

        [SetUp()]
        public void Setup()
        {
            inventoryTest = new();
            swordTest = new(new string[] { "sword" }, "sword", "a bronze sword");
            pcTest = new(new string[] { "pc" }, "computer", "a small computer");
            shovelTest = new(new string[] { "shovel" }, "shovel", "a long shovel");

            inventoryTest.Put(swordTest);
            inventoryTest.Put(pcTest);
            inventoryTest.Put(shovelTest);

            testList = $"\t{swordTest.ShortDescription}\n\t{pcTest.ShortDescription}\n\t{shovelTest.ShortDescription}\n";
        }

        [Test()]
        public void Test_Find_Item()
        {
            Assert.Multiple(() => {
                Assert.That(inventoryTest.HasItem("sword"), Is.True);
                Assert.That(inventoryTest.HasItem("pc"), Is.True);
                Assert.That(inventoryTest.HasItem("shovel"), Is.True);
            });
        }

        [Test()]
        public void Test_No_Item_Find()
        {
            Assert.Multiple(() => {
                Assert.That(inventoryTest.HasItem("bow"), Is.False);
                Assert.That(inventoryTest.HasItem("random"), Is.False);
            });
        }

        [Test()]
        public void Test_Fetch_Item()
        {
            Assert.Multiple(() => {
                Assert.That(swordTest, Is.EqualTo(inventoryTest.Fetch("sword")));
                Assert.That(pcTest, Is.EqualTo(inventoryTest.Fetch("pc")));

                Assert.That(inventoryTest.HasItem("sword"), Is.True);
                Assert.That(inventoryTest.HasItem("pc"), Is.True);
            });
        }

        [Test()]
        public void Test_Take_Item()
        {
            Assert.Multiple(() => {
                Assert.That(swordTest, Is.EqualTo(inventoryTest.Take("sword")));
                Assert.That(pcTest, Is.EqualTo(inventoryTest.Take("pc")));

                Assert.That(inventoryTest.HasItem("sword"), Is.False);
                Assert.That(inventoryTest.HasItem("pc"), Is.False);
            });
        }

        [Test()]
        public void Test_Item_List()
        {
            Assert.That(testList, Is.EqualTo(inventoryTest.ItemList));
        }
    }
}