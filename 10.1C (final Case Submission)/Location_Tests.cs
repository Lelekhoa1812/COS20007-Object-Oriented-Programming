//7.2C Test
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using SwinAdventure;

namespace LocationTests
{
    public class LocationTest
    {
        Player player = new Player("Fred", "the mighty programmer");
        Location home = new Location(new string[] { "home" }, "home", "home sweet home", "west");
        Location school = new Location(new string[] { "school" }, "school", "a study time", "east");
        Item sword = new Item(new string[] { "sword" }, "sword", "a bronze sword");
        Item pc = new Item(new string[] { "pc" }, "a computer", "a small computer");

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Location_is_Identifiable()
        {
            Assert.True(home.AreYou("home"));
        }

        [Test]
        public void Test_Location_isnot_Identifiable()
        {
            Assert.False(home.AreYou("school"));
        }

        [Test]
        public void Test_Location_locates_Itself()
        {
            player.Location = home;
            bool Output = home.AreYou("home");
            Assert.IsTrue(Output);
        }

        [Test]
        public void Test_Location_locates_Nothing()
        {
            player.Location = school;
            bool Output = school.AreYou("home");
            Assert.IsFalse(Output);
        }

        [Test]
        public void Test_PLayer_locates_Location()
        {
            player.Location = school;
            Assert.AreEqual(player.Locate("school"), school);

        }

        [Test]
        public void Test_Location_locates_Items()
        {
            home.Inventory.Put(sword);
            home.Inventory.Put(pc);
            GameObject exp1 = sword;
            GameObject exp2 = pc;
            GameObject Output1 = home.Locate("sword");
            GameObject Output2 = home.Locate("pc");
            Assert.AreEqual(Output1, exp1);
            Assert.AreEqual(Output2, exp2);
        }

        [Test]
        public void Test_Player_locates_Items()
        {
            player.Location = home;
            home.Inventory.Put(sword);
            home.Inventory.Put(pc);
            Assert.That(player.Locate("sword"), Is.SameAs(sword));
            Assert.That(player.Locate("pc"), Is.SameAs(pc));
        }
    }
}