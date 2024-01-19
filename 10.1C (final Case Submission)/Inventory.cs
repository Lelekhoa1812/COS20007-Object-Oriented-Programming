using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        private List<Item> _items = new List<Item>();

        public Inventory()
        {

        }

        public string ItemList
        {
            get
            {
                string list = "";
                foreach (Item itm in _items)
                {
                    list += "\t" + "a " + itm.Name + " " + "(" + itm.FirstID + ")\n";
                };
                return list;
            }
        }

        public bool HasItem(string id)
        {
            foreach (Item itm in _items)
            {
                if (itm.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }

        public Item Fetch(string id)
        {
            foreach (Item itm in _items)
            {
                if (itm.AreYou(id))
                {
                    return itm;
                }
            }
            return null;
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        public Item Take(string id)
        {
            Item itm = Fetch(id);

            if (_items != null)
            {
                _items.Remove(itm);
                return itm;
            }
            return null;
        }
    }
}