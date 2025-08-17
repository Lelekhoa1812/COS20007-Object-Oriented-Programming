using System;
using System.Collections.Generic;

namespace SwinAdventure
{
    //Identifiable Class
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public IdentifiableObject(string[] ids)
        {
            _identifiers = new List<string>(ids);
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }

        public string FirstId
        {
            get
            {
                if (_identifiers.Count > 0)
                {
                    return _identifiers[0];
                }
                else
                {
                    return "";
                }
            }
        }

        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }

        public static void Main(string[] args)
        {
            // Test Are You
            IdentifiableObject obj1 = new IdentifiableObject(new string[] { "fred", "bob" });
            Console.WriteLine(obj1.AreYou("fred")); // Should print True

            // Test Not Are You
            Console.WriteLine(obj1.AreYou("wilma")); // Should print False

            // Test Case Sensitive
            Console.WriteLine(obj1.AreYou("FRED")); // Should print True

            // Test First ID
            Console.WriteLine(obj1.FirstId); // Should print "fred"

            // Test First ID With No IDs
            IdentifiableObject obj2 = new IdentifiableObject(new string[] { });
            Console.WriteLine(obj2.FirstId); // Should print ""

            // Test Add ID
            obj1.AddIdentifier("wilma");
            Console.WriteLine(obj1.AreYou("wilma")); // Should print True

            //Create a player
            Player player = new Player("Fred", "the mighty programmer");

            // Create and add items to the player's inventory
            Item shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "This is a mighty fine shovel");
            Item sword = new Item(new string[] { "sword" }, "a bronze sword", "This is a shiny bronze sword");
            Item pc = new Item(new string[] { "pc" }, "a small computer", "This is an intelligent fast pc");
            player.Inventory.Put(shovel);
            player.Inventory.Put(sword);
            player.Inventory.Put(pc);

            // Print player's full description
            Console.WriteLine(player.FullDescription);

            // Locate an item in the player's inventory
            GameObject foundItem = player.Locate("shovel");
            if (foundItem != null)
            {
                Console.WriteLine($"Found: {foundItem.FullDescription}");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
    }


}