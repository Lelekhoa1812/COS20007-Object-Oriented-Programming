using System;
using System.Collections.Generic;

namespace SwinAdventure
{
    //GameObject Class
    using SwinAdventure;

    public class GameObject : IdentifiableObject
    {
        private string _name;
        private string _description;

        public GameObject(string[] ids, string name, string desc) : base(ids)
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
            get { return $"{_name} ({FirstId})"; }
        }

        public virtual string FullDescription
        {
            get { return _description; }
        }
    }

    // Item class
    public class Item : GameObject
    {
        public Item(string[] ids, string name, string desc) : base(ids, name, desc)
        {
        }
    }

    // Inventory class
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
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    _items.Remove(item);
                    return item;
                }
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
                    itemList += $"\t{item.ShortDescription}\n";
                }
                return itemList;
            }
        }
    }

    // Player class
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private IHaveInventory _location;

        public Player(string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
            _location = null;
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public IHaveInventory Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else if (_inventory.HasItem(id))
            {
                return _inventory.Take(id);
            }
            else if (_location != null && _location.Locate(id) != null)
            {
                return _location.Locate(id);
            }
            else
            {
                return null;
            }
        }

        public override string FullDescription
        {
            get { return $"You are {Name}, {base.FullDescription} You are carrying:\n{Inventory.ItemList}"; }
        }
    }

    // IHaveInventory interface
    public interface IHaveInventory
    {
        GameObject Locate(string id);
    }
}
