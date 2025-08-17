using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        Inventory _inventory = new Inventory();
        private Location _location;

        public Player(string name, string description) : base(new string[] { "me", "inventory" }, name, description)
        {

        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public override string FullDescription
        {
            get { return $"You are {Name} {base.FullDescription}.\nYou are carrying\n{Inventory.ItemList}"; }
        }

        public GameObject Locate(string id)
        {
            if (this.AreYou(id))
            {
                return this;
            }

            if (_inventory.Fetch(id) != null)
            {
                return _inventory.Fetch(id);
            }

            return _location == null ? null : _location.Locate(id);

        }

        //6.1P
        public Location Location
        {
            get => _location;
            set => _location = value;
        }

        //9.2C
        public void Move(Path path)
        {
            if (path.Destination != null)
            {
                _location = path.Destination;
            }
        }
    }
}