using SplashKitSDK;
using System;

namespace TicTacToe
{
    public class Player
    {
        public PlayerType Type { get; }

        public Player(PlayerType type)
        {
            Type = type;
        }
    }
}
