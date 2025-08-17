using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;
using Path = SwinAdventure.Path;

namespace MoveCommandTests
{
    public class MoveCommandTest
    {
        MoveCommand move;

        Path path1;
        Path path2;
        Player player;
        Location home;
        Location school;

        [SetUp]
        public void Setup()
        {
            move = new MoveCommand();
            player = new Player("Fred", "the mighty programmer");
            home = new Location(new string[] { "home" }, "home", "home sweet home", "west");
            school = new Location(new string[] { "school" }, "school", "a study time", "east");

            path1 = new Path(new string[] { "east" }, "east", "crowded village", school, home);
            path2 = new Path(new string[] { "west" }, "west", "quiet highway", home, school);

            school.AddPath(path1);
            home.AddPath(path2);
        }

        // The following tests experiment the go and move command simultaneously
        [Test]
        public void Test_Valid_Move()
        {
            player.Location = home;
            move.Execute(player, new string[] { "move", "west" });
            Assert.AreEqual(school.Name, player.Location.Name);

            player.Location = school;
            move.Execute(player, new string[] { "go", "east" });
            Assert.AreEqual(home.Name, player.Location.Name);
        }

        [Test]
        public void Test_Invalid_Move()
        {
            player.Location = home;
            move.Execute(player, new string[] { "move", "east" });
            Assert.AreNotEqual(school.Name, player.Location.Name);

            player.Location = school;
            move.Execute(player, new string[] { "go", "west" });
            Assert.AreNotEqual(home.Name, player.Location.Name);
        }

        [Test]
        public void Test_Successful_Move()
        {
            player.Location = school;
            string actual1 = move.Execute(player, new string[] { "move", "east" });
            string expected1 = "You have moved east, via a crowded village to the home";
            Assert.That(actual1, Is.EqualTo(expected1));

            player.Location = school;
            string actual2 = move.Execute(player, new string[] { "go", "east" });
            string expected2 = "You have moved east, via a quiet highway to the home";
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void Test_Incorrect_Command_Length()
        {
            string actual = move.Execute(player, new string[] { "move", "to", "north" });
            string expected = "Error in move input.";
            Assert.That(actual, Does.Contain(expected));
        }

        [Test]
        public void Test_Only_Move_Command()
        {
            string actual1 = move.Execute(player, new string[] { "move" });
            string expected1 = "Where do you want to move?";
            Assert.That(actual1, Is.EqualTo(expected1));

            string actual2 = move.Execute(player, new string[] { "go" });
            string expected2 = "Where do you want to move?";
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void Test_Invalid_Direction()
        {
            player.Location = home;
            string actual1 = move.Execute(player, new string[] { "move", "south" });
            string expected1 = "Invalid pathway";
            Assert.That(actual1, Is.EqualTo(expected1));

            player.Location = home;
            string actual2 = move.Execute(player, new string[] { "go", "north" });
            string expected2 = "Invalid pathway";
            Assert.That(actual2, Is.EqualTo(expected2));
        }
    }
}