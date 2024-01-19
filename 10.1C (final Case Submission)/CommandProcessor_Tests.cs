using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;
using Path = SwinAdventure.Path;

namespace CommandProcessorTest
{
    [TestFixture]
    public class TestCommandProcessor
    {
        CommandProcessor command;

        Path path1;
        Path path2;

        Location home;
        Location school;

        Item sword;
        Item pc;
        Item gem;
        Item book;

        Player player;

        Bag bag;

        [SetUp]
        public void Setup()
        {
            command = new();
            player = new Player("Fred", "the mighty programmer");

            //Move setup
            home = new Location(new string[] { "home" }, "home", "home sweet home", "west");
            school = new Location(new string[] { "school" }, "school", "a study time", "east");

            path1 = new Path(new string[] { "east" }, "east", "crowded village", school, home);
            path2 = new Path(new string[] { "west" }, "west", "empty highway", home, school);

            school.AddPath(path1);
            home.AddPath(path2);

            //Look setup
            sword = new Item(new string[] { "sword" }, "sword", "a bronze sword");
            pc = new Item(new string[] { "pc" }, "computer", "a small computer");
            gem = new Item(new string[] { "gem" }, "ruby", "a shining gem");
            book = new Item(new string[] { "book" }, "novel", "a lifetime autobiography");

            bag = new Bag(new string[] { "backpack" }, "backpack", "a big bag");
        }

        // LookCommand Test
        [Test]
        public void Test_Look_At_Item_in_Bag()
        {
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string actual = command.ExecuteCommand(player, new string[] { "look", "at", "gem", "in", $"backpack" });
            string expect = $"{gem.FullDescription}";
            Assert.AreEqual(actual, expect);
        }

        [Test]
        public void Test_Look_At_Unknow_Item_in_Bag()
        {
            player.Inventory.Put(bag);
            string actual = command.ExecuteCommand(player, new string[] { "look", "at", "sword", "in", $"backpack" });
            string expect = $"I can't find the sword";
            Assert.AreEqual(actual, expect);
        }

        [Test]
        public void Test_Look_At_No_Bag()
        {
            string Output = command.ExecuteCommand(player, new string[] { "look", "at", "backpack", "in", $"{player.FirstID}" });
            string exp = $"I can't find the backpack";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Item_in_Me()
        {
            player.Inventory.Put(book);
            player.Inventory.Put(pc);
            string actual1 = command.ExecuteCommand(player, new string[] { "look", "at", "book", "in", "me" });
            string actual2 = command.ExecuteCommand(player, new string[] { "look", "at", "pc", "in", "me" });
            string expect1 = $"{book.FullDescription}";
            string expect2 = $"{pc.FullDescription}";
            Assert.AreEqual(actual1, expect1);
            Assert.AreEqual(actual2, expect2);
        }

        [Test]
        public void Test_Invalid_Look()
        {
            Assert.AreEqual(command.ExecuteCommand(player, new string[] { "look", "around" }), "I don't know how to look like that.");
            Assert.AreEqual(command.ExecuteCommand(player, new string[] { "look", "by", "none" }), "What do you want to look at?");
        }

        [Test]
        public void Test_Player_Look_Location()
        {
            player.Location = home;
            home.Inventory.Put(gem);
            Assert.AreEqual(command.ExecuteCommand(player, new string[] { "look" }), $"You are at the home\nhome sweet home\nYou can see these items:\n{home.Inventory.ItemList}\n{home.ShortDescription}.");
        }

        // MoveCommand Test
        [Test]
        public void Test_Valid_Move()
        {
            player.Location = home;
            command.ExecuteCommand(player, new string[] { "move", "west" });
            Assert.AreEqual(school.Name, player.Location.Name);

            player.Location = school;
            command.ExecuteCommand(player, new string[] { "go", "east" });
            Assert.AreEqual(home.Name, player.Location.Name);
        }

        [Test]
        public void Test_Successful_Move()
        {
            player.Location = school;
            string actual1 = command.ExecuteCommand(player, new string[] { "move", "east" });
            string expected1 = "You have moved east, via a crowded village to the home";
            Assert.That(actual1, Is.EqualTo(expected1));

            player.Location = home;
            string actual2 = command.ExecuteCommand(player, new string[] { "go", "west" });
            string expected2 = "You have moved west, via a empty highway to the school";
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void Test_Incorrect_Command_Length()
        {
            string actual = command.ExecuteCommand(player, new string[] { "move", "to", "north" });
            string expected = "Error in move input.";
            Assert.That(actual, Does.Contain(expected));
        }

        [Test]
        public void Test_Only_Move_Command()
        {
            string actual1 = command.ExecuteCommand(player, new string[] { "move" });
            string expected1 = "Where do you want to move?";
            Assert.That(actual1, Is.EqualTo(expected1));

            string actual2 = command.ExecuteCommand(player, new string[] { "go" });
            string expected2 = "Where do you want to move?";
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void Test_Invalid_Direction()
        {
            player.Location = home;
            string actual1 = command.ExecuteCommand(player, new string[] { "move", "south" });
            string expected1 = "Invalid pathway";
            Assert.That(actual1, Is.EqualTo(expected1));

            player.Location = home;
            string actual2 = command.ExecuteCommand(player, new string[] { "go", "north" });
            string expected2 = "Invalid pathway";
            Assert.That(actual2, Is.EqualTo(expected2));
        }
    }
}