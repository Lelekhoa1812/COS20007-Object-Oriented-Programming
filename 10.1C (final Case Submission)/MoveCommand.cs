using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go"})
        {
        }

        public override string Execute(Player p, string[] text)
        {
            string error = "Error in move input.";

            if (text.Length > 2)
            {
                return error;
            }

            if (text.Length == 1)
            {
                return "Where do you want to move?";
            }

            else
            {
                string _moveDirection;

                switch (text.Length)
                {
                    case 1:
                        return "Move where?";
                    case 2:
                        _moveDirection = text[1].ToLower();
                        break;
                    case 3:
                        _moveDirection = text[2].ToLower();
                        break;
                    default:
                        return error;
                }

                Path _path = p.Location.findPath(_moveDirection);
                if (_path != null)
                {
                    if (_path.GetType() != typeof(Path))
                    {
                        return $"Could not find the {_path.Name} direction";
                    }

                    p.Location = _path.Destination;
                    p.Move((Path)_path);
                    return $"You have moved {_path.Name}, via {_path.ShortDescription} to the {p.Location.Name}";
                }

                else
                {
                    return "Invalid pathway";
                }
            }
        }
    }
}
