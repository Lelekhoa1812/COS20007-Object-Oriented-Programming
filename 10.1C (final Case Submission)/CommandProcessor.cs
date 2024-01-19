using System;
using System.Numerics;

namespace SwinAdventure
{
    public class CommandProcessor
    {
        List<Command> _commands;

        public CommandProcessor()
        {
            _commands = new List<Command>
            {
                new LookCommand(),
                new MoveCommand()
            };
        }

        public string ExecuteCommand(Player p, string[] text)
        {
            string input = text[0].ToLower();
            Command exeCommand = null;
            // loop to find the most suitable command
            foreach (Command command in _commands)
            {
                if (command.AreYou(input))
                {
                    exeCommand = command;
                    break;
                }
            }
            // if can't find the suitable command
            if (exeCommand == null)
            {
                return "I don't know how to " + input + ".";
            }
            return exeCommand.Execute(p, text);
        }
    }
}
