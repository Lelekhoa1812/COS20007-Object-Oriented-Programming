using SwinAdventure;
using NUnit.Framework;

namespace BagTests
{
    [TestFixture()]
    internal class TestBag
    {
        Bag b1, b2;
        Inventory inventoryTest;
        Item swordTest, pcTest, shovelTest;

        [SetUp()]
        public void Setup()
        {
            b1 = new(new string[] { "backpack" }, "backpack", "a big bag");
            b2 = new(new string[] { "crossbody" }, "crossbody", "a small bag");

            inventoryTest = new();
            swordTest = new(new string[] { "sword" }, "sword", "a bronze sword");
            pcTest = new(new string[] { "pc" }, "computer", "a small compute");
            shovelTest = new(new string[] { "shovel" }, "shovel", "a long shovel");

            b1.Inventory.Put(swordTest);
            b1.Inventory.Put(pcTest);

            b2.Inventory.Put(shovelTest);
        }

        [Test()]
        public void Test_Bag_Locates_Items()
        {
            Assert.Multiple(() => {

                Assert.That(b1.Locate("sword"), Is.EqualTo(swordTest));
                Assert.That(b1.Locate("pc"), Is.EqualTo(pcTest));

                Assert.IsTrue(b1.Inventory.HasItem("sword"));
                Assert.IsTrue(b1.Inventory.HasItem("pc"));
            });
        }

        [Test()]
        public void Test_Bag_Locates_itself()
        {
            Assert.That(b1, Is.EqualTo(b1.Locate("backpack")));
        }

        [Test()]
        public void Test_Bag_Locates_nothing()
        {
            Assert.That(b1.Locate("random"), Is.EqualTo(null));
        }

        [Test()]
        public void Test_Bag_Full_Description()
        {
            string testBagDescription = $"In the {b1.Name} you can see\n{b1.Inventory.ItemList}";
            Assert.That(b1.FullDescription, Is.EqualTo(testBagDescription));
        }

        [Test()]
        public void Test_Bag_in_Bag()
        {
            b1.Inventory.Put(b2);

            Assert.Multiple(() => {
                Assert.That(b1.Locate("crossbody"), Is.EqualTo(b2));

                Assert.That(b1.Locate("sword"), Is.EqualTo(swordTest));
                Assert.That(b1.Locate("pc"), Is.EqualTo(pcTest));

                Assert.That(b1.Locate("shovel"), Is.EqualTo(null));
            });
        }
    }
}