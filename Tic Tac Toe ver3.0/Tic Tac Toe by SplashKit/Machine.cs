using System;
using SplashKitSDK;

namespace TicTacToe
{
    public class Machine
    {
        public PlayerType Type { get; }

        public Machine(PlayerType type)
        {
            Type = type;
        }
    }
}
