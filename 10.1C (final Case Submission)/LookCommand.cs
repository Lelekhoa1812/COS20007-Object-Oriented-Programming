//6.1P Annotation
using System;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() :
            base(new string[] { "look" })
        {
        }

        public override string Execute(Player p, string[] text)
        {
            IHaveInventory _container;
            string _itemid = null;
            string error = "Error in look input.";

            if (text.Length != 1 && text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that.";
            }

            else
            {
                if (text[0] == "look" && text.Length == 1)
                {
                    return p.Location.FullDescription;                   
                }

                if (text[0].ToLower() != "look")
                    return error;

                // The second word must be "at"
                if (text.Length != 1 && text[1] != "at")
                {
                    return "What do you want to look at?";
                }
                // The fourth word must be "in"
                if (text.Length == 5 && text[3] != "in")
                {
                    return "What do you want to look in?";
                }

                switch (text.Length)
                {
                    //7.2C
                    case 1:
                        _container = p;
                        _itemid = "location"; 
                        break;

                    case 3:
                        _container = p;
                        _itemid = text[2];
                        break;

                    case 5:
                        _container = FetchContainer(p, text[4]);
                        if (_container == null)
                            return "I can't find the " + text[4];
                        _itemid = text[2];
                        break;

                    default:
                        _container = p;
                        break;
                }
                return LookAtIn(_itemid, _container);
            }
        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string LookAtIn(string thingId, IHaveInventory container)
        {
            if (container.Locate(thingId) != null)
                return container.Locate(thingId).FullDescription;

            return "I can't find the " + thingId;
        }
    }
}