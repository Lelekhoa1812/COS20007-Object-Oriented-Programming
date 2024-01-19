using SplashKitSDK;
using System;

namespace TicTacToe
{
    public class Program
    {
        public static void Main()
        {
            const int WindowWidth = 500;
            const int WindowHeight = 500;
            const int Rows = 5;
            const int Cols = 5;

            const int WinnerWindowWidth = 400;
            const int WinnerWindowHeight = 200;

            Window gameWindow = new Window("Tic Tac Toe", WindowWidth, WindowHeight);
            Drawing board = new Drawing(WindowWidth, WindowHeight, Rows, Cols);

            // Initialize the Menu
            Menu menu = new Menu(WindowWidth, WindowHeight);

            // Initialize the winner flag
            bool inGame = false;
            bool gameOver = true;
            bool gameOver3 = true;
            bool inWinner = false;

            WinnerWindow winWindow = null; // Declare the WinnerWindow object

            while (!gameWindow.CloseRequested)
            {
                if (!inGame)
                {
                    menu.Draw();
                    SplashKit.ProcessEvents();

                    if (menu.IsPlayerVsMachineModeClicked())
                    {
                        inGame = true;
                        gameOver = false;
                        menu.Hide();
                        board.SwitchToPlayerVsMachineMode();
                    }
                    else if (menu.IsPlayerVsPlayerModeClicked())
                    {
                        inGame = true;
                        gameOver = false;
                        menu.Hide();
                        board.SwitchToPlayerVsPlayerMode();
                    }
                    else if (menu.IsThreePlayerModeClicked())
                    {
                        inGame = true;
                        gameOver3 = false;
                        menu.Hide();
                        board.SwitchToThreePlayerMode();
                    }
                }
                else
                {
                    SplashKit.ProcessEvents();

                    if (inWinner)
                    {
                        // If a winner is detected, create the WinnerWindow
                        if (winWindow == null)
                        {
                            winWindow = new WinnerWindow(WinnerWindowWidth, WinnerWindowHeight);
                        }
                        winWindow.Draw();

                        // Handle the button click
                        if (winWindow.IsOKButtonClicked())
                        {
                            winWindow.Hide();
                            winWindow = null; // Close the WinnerWindow
                            inWinner = false;
                        }
                    }

                    if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                    {
                        board.ChangeGridColor();
                    }

                    if (SplashKit.KeyTyped(KeyCode.EscapeKey))
                    {
                        board.Reset();
                        if (board.IsPlayerVsMachineMode() || board.IsPlayerVsPlayerMode())
                        {
                            gameOver = false;
                        }

                        if (board.IsThreePlayerMode())
                        {
                            gameOver3 = false;
                        }
                    }

                    if (!gameOver && !inWinner)
                    {
                        if (SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            board.PlaceMarker();
                            var winner = board.CheckForWinner();
                            if (winner != PlayerType.Empty)
                            {
                                board.UpdateScores(winner);

                                if (board.IsPlayerVsMachineMode() || board.IsPlayerVsPlayerMode())
                                {
                                    inWinner = true;
                                    Console.WriteLine($"The Winner is {winner} side.");
                                    Console.WriteLine($"[X] {board.XScore} - {board.OScore} [O]");
                                }
                                gameOver = true;
                            }
                        }
                    }

                    if (!gameOver3)
                    {
                        if (SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            board.PlaceMarker();
                            var winner3 = board.ThreePlayerWinnerCheck();

                            if (winner3 != PlayerType.Empty)
                            {
                                board.UpdateScores3P(winner3);

                                if (board.IsThreePlayerMode())
                                {
                                    Console.WriteLine($"The Winner is {winner3} side.");
                                    Console.WriteLine($"[X] {board.XScore} - {board.OScore} [O] - {board.ZScore} [Z]");
                                }

                                gameOver3 = true;
                            }
                        }
                    }

                    gameWindow.Clear(Color.White);
                    board.Draw();
                    gameWindow.Refresh();
                }
            }
        }
    }


public enum PlayerType
    {
        X,
        O,
        Z,
        W,
        Empty
    }
}
