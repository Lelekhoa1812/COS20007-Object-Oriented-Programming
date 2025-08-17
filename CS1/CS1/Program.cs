using System;
using System.Collections.Generic;
using System.Numerics;
using SwinAdventure;

namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>(idents);
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id);
        }

        public string FirstId
        {
            get { return _identifiers.Count > 0 ? _identifiers[0] : ""; }
        }

        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }
    }

    public class GameObject : IdentifiableObject
    {
        private string _name;
        private string _description;

        public GameObject(string[] ids, string name, string desc)
            : base(ids)
        {
            _name = name;
            _description = desc;
        }

        public string Name
        {
            get { return _name; }
        }

        public string ShortDescription
        {
            get { return _name + " (" + FirstId + ")"; }
        }

        public virtual string FullDescription
        {
            get { return _description; }
        }
    }

    public class Item : GameObject
    {
        public Item(string[] ids, string name, string desc)
            : base(ids, name, desc)
        {
        }
    }

    public class Player : GameObject
    {
        private Inventory _inventory;

        public Player(string name, string desc)
            : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        //Remove the item 
        public void TakeItem(string itemName)
        {
            Item item = _inventory.Take(itemName);
            if (item != null)
            {
                Console.WriteLine($"You take {item.Name}.");
            }
            else
            {
                Console.WriteLine($"You cannot take {itemName}.");
            }
        }

        //Add the item
        public void PutItem(string itemName)
        {
            Item item = new Item(new string[] { "item" }, $"a {itemName}", $"This is {itemName}");
            _inventory.Put(item);
            Console.WriteLine($"You put {itemName} into your bag.");
        }

        public override string FullDescription
        {
            get
            {
                string description = base.FullDescription;
                description += "You are carrying:";
                description += _inventory.ItemList;
                return description;
            }
        }
    }

    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public bool HasItem(string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        public Item Take(string id)
        {
            Item itemToRemove = null;
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
                return itemToRemove;
            }
            return null;
        }

        public string ItemList
        {
            get
            {
                string itemList = "";
                foreach (Item item in _items)
                {
                    itemList += "\n\t" + item.ShortDescription;
                }
                return itemList;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = null;

            while (true)
            {
                string playerName = Console.ReadLine();

                if (playerName.ToLower() == "fred")
                {
                    Console.WriteLine("True");
                    player = new Player("Fred", "the mighty programmer");
                    player.Inventory.Put(new Item(new string[] { "pc" }, "a small computer", "This is a small compactable computer"));
                    player.Inventory.Put(new Item(new string[] { "shirt" }, "a white t-shirt", "This is a flowerish white t-shirt"));
                    Console.WriteLine("You are Fred, the mighty programmer");
                    Console.WriteLine("You are carrying:");
                    Console.WriteLine(player.Inventory.ItemList);
                }
                else if (playerName.ToLower() == "bob")
                {
                    Console.WriteLine("True");
                    player = new Player("Bob", "the mighty knight");
                    player.Inventory.Put(new Item(new string[] { "shield" }, "a sturdy shield", "This is a solid shield"));
                    player.Inventory.Put(new Item(new string[] { "sword" }, "a bronze sword", "This is a shiny bronze sword"));
                    Console.WriteLine("You are Bob, the mighty knight");
                    Console.WriteLine("You are carrying:");
                    Console.WriteLine(player.Inventory.ItemList);
                }
                else
                {
                    Console.WriteLine("False");
                }

            }

            if (player != null)
            {
                while (true)
                {
                    //Console.Write("Enter a command (take/put [item], find [item], or 'exit' to quit): ");
                    string command = Console.ReadLine();

                    if (command.ToLower() == "exit")
                    {
                        break;
                    }

                    string[] parts = command.Split(' ');
                    if (parts.Length >= 2)
                    {
                        string action = parts[0].ToLower();
                        string item = string.Join(" ", parts, 1, parts.Length - 1).ToLower();

                        if (action == "take")
                        {
                            player.TakeItem(item);
                        }
                        else if (action == "put")
                        {
                            player.PutItem(item);
                        }
                        else if (action == "find")
                        {
                            Item foundItem = player.Inventory.Take(item);
                            if (foundItem != null)
                            {
                                Console.WriteLine(foundItem.FullDescription);
                            }
                            else
                            {
                                Console.WriteLine($"You cannot find {item}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }
                }
            }
        }
    }
}










