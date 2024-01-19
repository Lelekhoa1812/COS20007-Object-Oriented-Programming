using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class GameObject : IdentifiableObject
    {
        private string _name;
        private string _description;

        public GameObject(string[] ids, string name, string description) : base(ids)
        {
            _name = name;
            _description = description;
        }

        public string Name
        {
            get { return _name; }
        }

        public virtual string ShortDescription
        {
            get
            {
                return "a " + _name + " " + "(" + FirstID + ")".ToLower();
            }
        }

        public virtual string FullDescription
        {
            get { return _description; }
        }
    }
}