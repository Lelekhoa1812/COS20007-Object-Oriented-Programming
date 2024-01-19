using SwinAdventure;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;

namespace PlayerTests
{
    [TestFixture()]
    internal class TestPlayer
    {
        Player playerTest;
        Inventory inventoryTest;
        Item swordTest, pcTest;
        Location home;
        string testDescription;

        [SetUp()]
        public void Setup()
        {
            playerTest = new("Fred", "the mighty programmer");
            inventoryTest = new();
            swordTest = new(new string[] { "sword" }, "sword", "a bronze sword");
            pcTest = new(new string[] { "pc" }, "computer", "a small computer");

            playerTest.Inventory.Put(swordTest);
            playerTest.Inventory.Put(pcTest);

            home = new Location(new string[] { "home" }, "home", "home sweet home", "no exit");
            home.Inventory.Put(swordTest);
            playerTest.Location = home;

            testDescription = $"You are Fred the mighty programmer.\nYou are carrying\n{playerTest.Inventory.ItemList}";
        }

        [Test()]
        public void Test_Player_is_Identifiable()
        {
            Assert.IsTrue(playerTest.AreYou("me"));
        }

        [Test()]
        public void Test_Player_Locates_Items()
        {
            Assert.Multiple(() => {
                Assert.That(playerTest.Locate("sword"), Is.EqualTo(swordTest));
                Assert.That(playerTest.Locate("pc"), Is.EqualTo(pcTest));

                Assert.That(playerTest.Inventory.HasItem("sword"), Is.True);
                Assert.That(playerTest.Inventory.HasItem("pc"), Is.True);
            });
        }

        [Test()]
        public void Test_Player_Locates_Itself()
        {
            Assert.That(playerTest, Is.EqualTo(playerTest.Locate("me")));
            Assert.That(playerTest, Is.EqualTo(playerTest.Locate("inventory")));
        }

        [Test()]
        public void TestPlayer_Locates_Nothing()
        {
            Assert.That(playerTest.Locate("random"), Is.EqualTo(null));
        }


        [Test()]
        public void Test_Player_Full_Description()
        {
            Assert.That(playerTest.FullDescription, Is.EqualTo(testDescription));
        }

        //7.2C Test
        [Test]
        public void Test_Location_locates_Itself()
        {
            Assert.That(playerTest.Locate("home"), Is.SameAs(home));
        }

        [Test]
        public void Test_locate_Item_in_Location()
        {
            Assert.That(home.Locate("sword"), Is.SameAs(swordTest));
        }
    }
}
