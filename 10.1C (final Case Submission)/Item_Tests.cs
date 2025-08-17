using SwinAdventure;
using NUnit.Framework;

namespace ItemTests
{
    [TestFixture()]
    internal class TestItem
    {
        Item itemTest;
        String testShortDescription;

        [SetUp()]
        public void Setup()
        {
            itemTest = new(new string[] { "sword" }, "sword", "a bronze sword");
            testShortDescription = "a sword (sword)";
        }

        [Test()]
        public void Test_Item_is_Identifiable()
        {
            Assert.That(itemTest.AreYou("sword"), Is.True);
        }

        [Test()]
        public void Test_Short_Description()
        {
            Assert.That(itemTest.ShortDescription, Is.EqualTo(testShortDescription));
        }

        [Test()]
        public void Test_Full_Description()
        {
            Assert.That(itemTest.FullDescription, Is.EqualTo("a bronze sword"));
        }
    }
}