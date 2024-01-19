using System.Numerics;
using SwinAdventure;
using Path = SwinAdventure.Path;

namespace PathTests
{
    [TestFixture]
    public class PathTest
    {
        Path path1;
        Path path2;
        Location home;
        Location school;
        Player player;

        [SetUp]
        public void Setup()
        {
            Player player = new Player("Fred", "the mighty programmer");
            Location home = new Location(new string[] { "home" }, "home", "home sweet home", "west");
            Location school = new Location(new string[] { "school" }, "school", "a study time", "east");

            path1 = new Path(new string[] { "east" }, "east", "east direction", school, home);
            path2 = new Path(new string[] { "west" }, "west", "west direction", home, school);
        }

        [Test]
        public void Test_Path_is_Identifiable()
        {
            Assert.That(path1.AreYou("east"), Is.True);
            Assert.That(path1.AreYou("north"), Is.False);
        }

        [Test]
        public void Test_Path_FullDescription()
        {
            string Output1 = path1.FullDescription;
            string exp1 = "the home is in the east direction";
            Assert.That(Output1, Is.EqualTo(exp1));

            string Output2 = path2.FullDescription;
            string exp2 = "the school is in the west direction";
            Assert.That(Output2, Is.EqualTo(exp2));
        }

        [Test]
        public void Test_Path_Wrong_FullDescription()
        {
            string Output1 = path1.FullDescription;
            string exp1 = "the home is in the west direction";
            Assert.That(Output1, Is.Not.EqualTo(exp1));

            string Output2 = path1.FullDescription;
            string exp2 = "the school is in the east direction";
            Assert.That(Output2, Is.Not.EqualTo(exp2));
        }

        public void Test_locate_Path()
        {
            player.Location = home;
            home.AddPath(path1);
            Assert.AreEqual(home.Locate("east"), path1);

            player.Location = home;
            home.AddPath(path2);
            Assert.AreEqual(home.Locate("west"), path2);
        }
    }
}
