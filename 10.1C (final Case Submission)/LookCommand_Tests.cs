//6.1P Annotation
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using SwinAdventure;

namespace LookCommandTests
{
    public class LookCommandTest
    {
        Command look;
        Player player;
        Bag bag;

        Item sword = new Item(new string[] { "sword" }, "a sword", "a bronze sword");
        Item pc = new Item(new string[] { "pc" }, "a computer", "a small computer");
        Item gem = new Item(new string[] { "gem" }, "a gem", "a shining gem");

        Location home = new Location(new string[] { "home" }, "home", "home sweet home", "no exit");

        [SetUp]
        public void Setup()
        {
            look = new LookCommand();
            player = new Player("Fred", "the mighty programmer");
            bag = new Bag(new string[] { "bag" },
                $"Fred's bag",
                $"This is {player.FirstID} bag");
        }

        [Test]
        public void Test_Look_At_Me()
        {
            string Output = look.Execute(player, new string[] { "look", "at", "inventory" });
            string exp = $"You are Fred the mighty programmer.\nYou are carrying\n{player.Inventory.ItemList}";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Gem()
        {
            player.Inventory.Put(bag);
            player.Inventory.Put(gem);
            string Output = look.Execute(player, new string[] { "look", "at", "gem" });
            string exp = $"{gem.FullDescription}";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Unk()
        {
            string Output = look.Execute(player, new string[] { "look", "at", "gem" });
            string exp = $"I can't find the gem";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Gem_in_Me()
        {
            player.Inventory.Put(gem);
            string Output = look.Execute(player, new string[] { "look", "at", "gem", "in", $"{player.FirstID}" });
            string exp = $"{gem.FullDescription}";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Gem_in_Bag()
        {
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string Output = look.Execute(player, new string[] { "look", "at", "gem", "in", $"bag" });
            string exp = $"{gem.FullDescription}";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_Gem_in_No_Bag()
        {
            bag.Inventory.Put(gem);
            //player1.Inventory.Put(bag);
            string Output = look.Execute(player, new string[] { "look", "at", "bag", "in", $"{player.FirstID}" });
            string exp = $"I can't find the bag";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Look_At_No_Gem_in_Bag()
        {
            player.Inventory.Put(bag);
            //bag.Inventory.Put(gem);
            string Output = look.Execute(player, new string[] { "look", "at", "gem", "in", $"bag" });
            string exp = $"I can't find the gem";
            Assert.AreEqual(exp, Output);
        }

        [Test]
        public void Test_Invalid_Look()
        {
            Assert.AreEqual(look.Execute(player, new string[] { "look", "around" }), "I don't know how to look like that.");
            Assert.AreEqual(look.Execute(player, new string[] { "look", "by", "none" }), "What do you want to look at?");
            Assert.AreEqual(look.Execute(player, new string[] { "hello", "at", "b" }), "Error in look input.");

        }

        //7.2C Test
        [Test]
        public void Test_Player_Look_Location()
        {
            player.Location = home;
            home.Inventory.Put(gem);
            Assert.AreEqual(look.Execute(player, new string[] { "look" }), $"You are at the home\n{home.FullDescription}\nYou can see these items:\n{home.Inventory.ItemList}\n{home.ShortDescription}");
        }

    }
}