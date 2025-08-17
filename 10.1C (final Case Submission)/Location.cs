//7.2C
using System;
using System.Collections.Generic;
using System.IO;
using SwinAdventure;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private List<Path> _path;
        private string _desc;
        private string _exit;

        public Location(string[] ids, string name, string desc, string exit) : base(ids, name, desc)
        {
            _inventory = new Inventory();
            _path = new List<Path>();
            _desc = desc;
            _exit = exit;

        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }

            foreach (Path p in _path)
            {
                if (p.AreYou(id))
                {
                    return p;
                }
            }
            return _inventory.Fetch(id);
        }
        public override string ShortDescription
        {
            get
            {
                return $"There are exits via the {_exit}";
            }
        }

        public override string FullDescription
        {
            get
            {
                return $"You are at the {Name}\n{_desc}\nYou can see these items:\n{_inventory.ItemList}\n{ShortDescription}.";
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        //9.2C
        public Path findPath(string path)
        {
            foreach (Path p in _path)
            {
                if (p.AreYou(path))
                {
                    return p;
                }
            }
            return null;
        }

        public void AddPath(Path path)
        {
            _path.Add(path);
        }

        public string PathList
        {
            get
            {
                string list = string.Empty + "\n";

                if (_path.Count == 1)
                {
                    return "There is an exit " + _path[0].FirstID + ".";
                }

                list = list + "There are exits to the ";

                for (int i = 0; i < _path.Count; i++)
                {
                    if (i == _path.Count - 1)
                    {
                        list = list + "and " + _path[i].FirstID + ".";
                    }
                    else
                    {
                        list = list + _path[i].FirstID + ", ";
                    }
                }

                return list;
            }
        }
    }
}
