using System;

namespace SwinAdventure
{
    public class Path : GameObject
    {
        private Location _source, _destination;
        private string _desc;

        public Path(string[] ids, string name, string desc, Location source, Location destination) : base(ids, name, desc)
        {
            _destination = destination;
            _source = source;
            _desc = desc;
        }

        public Location Source
        {
            get
            {
                return _source;
            }
        }

        public Location Destination
        {
            get
            {
                return _destination;
            }
        }
        public override string ShortDescription
        {
            get
            {
                return $"a {_desc}";
            }
        }

        public override string FullDescription
        {
            get
            {
                return $"the {Destination.Name} is in the {Name} direction";
            }
        }       

        public string Move(Player player)
        {
            player.Location = Destination;
            return Destination.Name;
        }
    }
}
