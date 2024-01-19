using System;
using SplashKitSDK;

namespace TicTacToe
{
	public class Strategy
	{
		public Strategy()
		{
            /* Belows are the logical strategies of PlayerMachine (AI) in the PvM game mode
			 * Strategy 1 (Moderating): Seize the central square of the board if possible.
			 * Strategy 2 (Attacking): Once having a 2-a-kind streak of O, try to make the third.
			 * Strategy 3 (Attacking): Once having a 3-a-kind streak of O, try to make the forth.
			 * Strategy 4 (Defensive): Once confronting a 4-a-kind streak, try to avoid the loss.
			 * Strategy ∅ (Moderating): Make random moves if doesn't met any other strats. 
			 */
        }
    }
}