using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Data;

namespace TicTacToe
{
    public class Drawing
    {
        private PlayerType[,] board;
        private List<Player> markers = new List<Player>();

        private Color _gridColor = Color.Black;
        private int _gridSize;

        public bool IsPlayerVsMachineMode()
        {
            return _vsMachineMode;
        }

        public bool IsPlayerVsPlayerMode()
        {
            return _vsPlayerMode;
        }

        public bool IsThreePlayerMode()
        {
            return _threePlayerMode;
        }

        // Checking streak status
        private bool _horizontalStreak2O = false;
        private bool _verticalStreak2O = false;
        private bool _TLBRStreak2O = false;
        private bool _TRBLStreak2O = false;

        private bool _horizontalStreak3O = false;
        private bool _verticalStreak3O = false;
        private bool _TLBRStreak3O = false;
        private bool _TRBLStreak3O = false;

        private bool _horizontalStreakX = false;
        private bool _verticalStreakX = false;
        private bool _TLBRStreakX = false;
        private bool _TRBLStreakX = false;

        // Add score variables
        public int XScore { get; private set; }
        public int OScore { get; private set; }
        public int ZScore { get; private set; }

        // Variable to track gamemode
        private bool _vsMachineMode = false;
        private bool _vsPlayerMode = false;
        private bool _threePlayerMode = false;

        // First Player to play is X
        private PlayerType _currentPlayer = PlayerType.X;

        public Drawing(int windowWidth, int windowHeight, int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            SquareSize = windowWidth / cols;
            board = new PlayerType[Rows, Cols];
            InitializeBoard();

            XScore = 0;
            OScore = 0;
            ZScore = 0;
        }

        public int Rows { get; }
        public int Cols { get; }
        public int SquareSize { get; }

        private void InitializeBoard()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    board[row, col] = PlayerType.Empty;
                }
            }
        }

        public void ChangeGridColor()
        {
            _gridColor = SplashKit.RandomRGBColor(255);
        }

        //Drawing game's board and marker types
        public void Draw()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                var marker = board[row, col];
                int x = col * SquareSize;
                int y = row * SquareSize;
                float radius = SquareSize / 7;

                    if (marker == PlayerType.X)
                    {
                        SplashKit.DrawText("X", Color.Blue, "Arial", 72, x + SquareSize / 2.2, y + SquareSize / 2.2);
                    }
                    else if (marker == PlayerType.O)
                    {
                        SplashKit.DrawText("O", Color.Red, "Arial", 72, x + SquareSize / 2.2, y + SquareSize / 2.2);
                    }
                    else if (marker == PlayerType.Z) 
                    {
                        SplashKit.DrawText("Z", Color.Green, "Arial", 72, x + SquareSize / 2.2, y + SquareSize / 2.2);
                    }

                    else if (marker == PlayerType.W)
                    {
                        SplashKit.DrawText("W", Color.Orange, "Arial", 72, x + SquareSize / 2.2, y + SquareSize / 2.2);
                    }

                    SplashKit.DrawRectangle(_gridColor, x, y, SquareSize, SquareSize);
                }
            }
        }

        // Game play method for all modes
        public void PlaceMarker()
        {
            int mouseX = (int)SplashKit.MouseX();
            int mouseY = (int)SplashKit.MouseY();

            int row = mouseY / SquareSize;
            int col = mouseX / SquareSize;

            //Player vs Player Mode
            if (row >= 0 && row < Rows && col >= 0 && col < Cols && board[row, col] == PlayerType.Empty && _vsPlayerMode)
            {
                // Player X moves at odd-numbered and O moves at even-numbered moves
                var type = markers.Count % 2 == 0 ? PlayerType.X : PlayerType.O;
                board[row, col] = type;
                markers.Add(new Player(type));

            }

            //Player vs Machine Mode
            if (row >= 0 && row < Rows && col >= 0 && col < Cols && board[row, col] == PlayerType.Empty && _vsMachineMode)
            {
                // PlayerMachine makes moves at even-numbered moves
                var type = markers.Count % 2 == 0 ? PlayerType.X : PlayerType.O;
                board[row, col] = type;
                markers.Add(new Player(type));

                if (_vsMachineMode && type == PlayerType.X)
                {
                    int streakRow2O, streakCol2O;
                    bool canCompleteStreak2O = FindStreak2(board, PlayerType.O, out streakRow2O, out streakCol2O);

                    int streakRowO, streakColO;
                    bool canCompleteStreak3O = FindStreak3(board, PlayerType.O, out streakRowO, out streakColO);

                    int stopRowX, stopColX;
                    bool canStopStreak3X = StopStreak3(board, PlayerType.X, out stopRowX, out stopColX);

                    // Strategy 1 (M)
                    if (markers.Count == 1)
                    {
                        int centerRow = Rows / 2;
                        int centerCol = Cols / 2;

                        if (centerRow >= 0 && centerRow < Rows && centerCol >= 0 && centerCol < Cols && board[centerRow, centerCol] == PlayerType.Empty)
                        {
                            board[centerRow, centerCol] = PlayerType.O;
                            markers.Add(new Player(PlayerType.O));
                        }
                        else
                        {
                            MakeMachineMove();
                        }
                    }
                   
                    else if (markers.Count >= 3)
                    {
                        // Strategy 3 (A)
                        if (canCompleteStreak3O)
                        {
                            Console.WriteLine($"streakRow3O: {streakRowO}, streakCol3O: {streakColO}");

                            int targetRow = streakRowO;
                            int targetCol = streakColO;

                            if (streakRowO >= 0 && streakRowO < Rows && streakColO >= 0 && streakColO < Cols)
                            {
                                // Check available horizontal streak
                                if (_horizontalStreak3O)
                                {
                                    Console.WriteLine("Streak 3 of O horizontal");
                                    if (streakColO + 1 >= 0 && board[streakRowO, streakColO + 1] == PlayerType.Empty)
                                    {
                                        targetCol = streakColO + 1;
                                    }
                                    else if (streakColO - 3 < Cols && board[streakRowO, streakColO - 3] == PlayerType.Empty)
                                    {
                                        targetCol = streakColO - 3;
                                    }
                                }

                                // Check available vertical streak
                                else if (_verticalStreak3O)
                                {
                                    Console.WriteLine("Streak 3 of O vertical");
                                    if (streakRowO + 1 >= 0 && board[streakRowO + 1, streakColO] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO + 1;
                                    }
                                    else if (streakRowO - 3 < Rows && board[streakRowO - 3, streakColO] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO - 3;
                                    }
                                }

                                // Check available diagonal tl-br streak
                                else if (_TLBRStreak3O)
                                {
                                    Console.WriteLine("Streak 3 of O diagonal tl-br");
                                    if (streakRowO + 1 >= 0 && streakColO + 1 >= 0 && board[streakRowO + 1, streakColO + 1] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO + 1;
                                        targetCol = streakColO + 1;
                                    }
                                    else if (streakRowO - 3 < Rows && streakColO - 3 < Cols && board[streakRowO - 3, streakColO - 3] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO - 3;
                                        targetCol = streakColO - 3;
                                    }
                                }
                                // Check available diagonal tr-bl streak
                                else if (_TRBLStreak3O)
                                {
                                    Console.WriteLine("Streak 3 of O diagonal tr-bl");
                                    if (streakRowO - 1 >= 0 && streakColO - 1 < Cols && board[streakRowO - 1, streakColO - 1] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO - 1;
                                        targetCol = streakColO - 1;
                                    }
                                    else if (streakRowO + 3 < Rows && streakColO + 3 >= 0 && board[streakRowO + 3, streakColO + 3] == PlayerType.Empty)
                                    {
                                        targetRow = streakRowO + 3;
                                        targetCol = streakColO + 3;
                                    }
                                }

                                if (targetRow >= 0 && targetRow < Rows && targetCol >= 0 && targetCol < Cols && board[targetRow, targetCol] == PlayerType.Empty)
                                {
                                    board[targetRow, targetCol] = PlayerType.O;
                                    markers.Add(new Player(PlayerType.O));
                                    Console.WriteLine("O Executed");
                                    return;
                                }

                                else
                                {
                                    MakeMachineMove();
                                }

                            }
                        }

                        // Strategy 2 (A)
                        else if (canCompleteStreak2O && !canCompleteStreak3O && !canStopStreak3X)
                        {
                            Console.WriteLine($"streakRow2O: {streakRow2O}, streakCol2O: {streakCol2O}");

                            int targetRow2 = streakRow2O;
                            int targetCol2 = streakCol2O;
                            if (streakRow2O >= 0 && streakRow2O < Rows && streakCol2O >= 0 && streakCol2O < Cols)
                            {
                                // Check available horizontal streak
                                if (_horizontalStreak2O)
                                {
                                    Console.WriteLine("Streak 2 of O horizontal");
                                    if (streakCol2O + 1 >= 0 && board[streakRow2O, streakCol2O + 1] == PlayerType.Empty)
                                    {
                                        targetCol2 = streakCol2O + 1;
                                    }
                                    else if (streakCol2O - 2 < Cols && board[streakRow2O, streakCol2O - 2] == PlayerType.Empty)
                                    {
                                        targetCol2 = streakCol2O - 2;
                                    }
                                }

                                // Check available vertical streak
                                else if (_verticalStreak2O)
                                {
                                    Console.WriteLine("Streak 2 of O vertical");
                                    if (streakRow2O + 1 >= 0 && board[streakRow2O + 1, streakCol2O] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O + 1;
                                    }
                                    else if (streakRow2O - 2 < Rows && board[streakRow2O - 2, streakCol2O] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O - 2;
                                    }
                                }

                                // Check available diagonal tl-br streak
                                else if (_TLBRStreak2O)
                                {
                                    Console.WriteLine("Streak 2 of O diagonal tl-br");
                                    if (streakRow2O + 1 >= 0 && streakCol2O + 1 >= 0 && board[streakRow2O + 1, streakCol2O + 1] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O + 1;
                                        targetCol2 = streakCol2O + 1;
                                    }
                                    else if (streakRow2O - 2 < Rows && streakCol2O - 2 < Cols && board[streakRow2O - 2, streakCol2O - 2] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O - 2;
                                        targetCol2 = streakCol2O - 2;
                                    }
                                }
                                // Check available diagonal tr-bl streak
                                else if (_TRBLStreak2O)
                                {
                                    Console.WriteLine("Streak 2 of O diagonal tr-bl");
                                    if (streakRow2O - 1 >= 0 && streakCol2O - 1 < Cols && board[streakRow2O - 1, streakCol2O - 1] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O - 1;
                                        targetCol2 = streakCol2O - 1;
                                    }
                                    else if (streakRow2O + 2 < Rows && streakCol2O + 2 >= 0 && board[streakRow2O + 2, streakCol2O + 2] == PlayerType.Empty)
                                    {
                                        targetRow2 = streakRow2O + 2;
                                        targetCol2 = streakCol2O + 2;
                                    }
                                }

                                if (targetRow2 >= 0 && targetRow2 < Rows && targetCol2 >= 0 && targetCol2 < Cols && board[targetRow2, targetCol2] == PlayerType.Empty)
                                {
                                    canCompleteStreak2O = false;
                                    board[targetRow2, targetCol2] = PlayerType.O;
                                    markers.Add(new Player(PlayerType.O));
                                    Console.WriteLine("O2 Executed");
                                    return;
                                }

                                else
                                {
                                    MakeMachineMove();
                                }

                            }
                        }

                        //Strategy 4 (D)
                        else if (canStopStreak3X && !canCompleteStreak3O)
                        {
                            Console.WriteLine($"streakRowX: {stopRowX}, streakColX: {stopColX}");

                            int atRow = stopRowX;
                            int atCol = stopColX;
                            if (stopRowX >= 0 && stopRowX < Rows && stopColX >= 0 && stopColX < Cols)
                            {
                                // Check available horizontal streak
                                if (_horizontalStreakX)
                                {
                                    Console.WriteLine("Streak 3 of X horizontal");
                                    if (stopColX + 1 >= 0 && board[stopRowX, stopColX + 1] == PlayerType.Empty)
                                    {
                                        atCol = stopColX + 1;
                                    }
                                    else if (stopColX - 3 < Cols && board[stopRowX, stopColX - 3] == PlayerType.Empty)
                                    {
                                        atCol = stopColX - 3;
                                    }
                                }

                                // Check available vertical streak
                                if (_verticalStreakX)
                                {
                                    Console.WriteLine("Streak 3 of X vertical");
                                    if (stopRowX + 1 >= 0 && board[stopRowX + 1, stopColX] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX + 1;
                                    }
                                    else if (stopRowX - 3 < Rows && board[stopRowX - 3, stopColX] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX - 3;
                                    }
                                }

                                // Check available diagonal tl-br streak
                                if (_TLBRStreakX)
                                {
                                    Console.WriteLine("Streak 3 of X diagonal tl-br");
                                    if (stopRowX + 1 >= 0 && stopColX + 1 >= 0 && board[stopRowX + 1, stopColX + 1] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX + 1;
                                        atCol = stopColX + 1;
                                    }
                                    else if (stopRowX - 3 < Rows && stopColX - 3 < Cols && board[stopRowX - 3, stopColX - 3] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX - 3;
                                        atCol = stopColX - 3;
                                    }
                                }

                                // Check available diagonal tr-bl streak
                                if (_TRBLStreakX)
                                {
                                    Console.WriteLine("Streak 3 of X diagonal tr-bl");
                                    if (stopRowX - 1 >= 0 && stopColX - 1 < Cols && board[stopRowX - 1, stopColX -1] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX - 1;
                                        atCol = stopColX - 1 ;
                                    }
                                    else if (stopRowX + 3 < Rows && stopColX + 3 >= 0 && board[stopRowX + 3, stopColX + 3] == PlayerType.Empty)
                                    {
                                        atRow = stopRowX + 3;
                                        atCol = stopColX + 3;
                                    }
                                }

                                if (atRow >= 0 && atRow < Rows && atCol >= 0 && atCol < Cols && board[atRow, atCol] == PlayerType.Empty)
                                {
                                    board[atRow, atCol] = PlayerType.O;
                                    markers.Add(new Player(PlayerType.O));
                                    Console.WriteLine("OX Executed");
                                    return;
                                }

                                else
                                {
                                    MakeMachineMove();
                                }
                            }
                        }

                        else
                        {
                            MakeMachineMove();
                        }
                    }
                }
            }

            // 3 Player Mode
            else if (row >= 0 && row < Rows && col >= 0 && col < Cols && board[row, col] == PlayerType.Empty && _threePlayerMode)
            {
                var type = _currentPlayer;
                var winner3 = ThreePlayerWinnerCheck();

                board[row, col] = type;
                markers.Add(new Player(type));
                _currentPlayer = (PlayerType)(((int)_currentPlayer + 1) % 3);
            }
        }

        private bool FindStreak2(PlayerType[,] grid, PlayerType player, out int row, out int col)
        {
            row = -1;
            col = -1;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (grid[r, c] == player)
                    {
                        // Check horizontally for a streak of 2
                        if (c + 1 < Cols)
                        {
                            bool leftEmpty = c == 0 || grid[r, c - 1] == PlayerType.Empty;
                            bool rightEmpty = c + 2 >= Cols || grid[r, c + 2] == PlayerType.Empty;
                            if (((c != 1 && leftEmpty) && (c != 4 && rightEmpty)) && grid[r, c + 1] == player)
                            {
                                _horizontalStreak2O = true;
                                row = r;
                                col = c + 1;
                                return true;
                            }
                        }

                        // Check vertically for a streak of 2
                        if (r + 1 < Rows)
                        {
                            bool topEmpty = r == 0 || grid[r - 1, c] == PlayerType.Empty;
                            bool bottomEmpty = r + 2 >= Rows || grid[r + 2, c] == PlayerType.Empty;
                            if (((1 != 4 && topEmpty) && (r != 4 && bottomEmpty)) && grid[r + 1, c] == player)
                            {
                                _verticalStreak2O = true;
                                row = r + 1;
                                col = c;
                                return true;
                            }
                        }

                        // Check diagonally (tl-br) for a streak of 2
                        if (r + 1 < Rows && c + 1 < Cols && r - 1 >= 0 && c - 1 >= 0)
                        {
                            bool topLeftEmpty = grid[r - 1, c - 1] == PlayerType.Empty;
                            bool bottomRightEmpty = grid[r + 1, c + 1] == PlayerType.Empty;
                            if (((topLeftEmpty && c != 1 && r != 1) && (bottomRightEmpty && c != 4 && r != 4)) && grid[r + 1, c + 1] == player)
                            {
                                _TLBRStreak2O = true;
                                row = r + 1;
                                col = c + 1;
                                return true;
                            }
                        }

                        // Check diagonally (tr-bl) for a streak of 2
                        if (r + 1 < Rows && c - 1 >= 0 && r - 1 >= 0 && c + 1 < Cols)
                        {
                            bool topRightEmpty = grid[r - 1, c + 1] == PlayerType.Empty;
                            bool bottomLeftEmpty = grid[r + 1, c - 1] == PlayerType.Empty;
                            if (((topRightEmpty && c != 4 && r != 1) && (bottomLeftEmpty && c != 1 && r != 4)) && grid[r + 1, c - 1] == player)
                            {
                                _TRBLStreak2O = true;
                                row = r + 1;
                                col = c - 1;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        //Although the finding streak of 3 methods for either marker X and O are basically identical;
        //Yet they have to be separated to allows 2 different streak detection functions operate simultaneously.
        private bool FindStreak3(PlayerType[,] grid, PlayerType player, out int row, out int col)
        {
            row = -1;
            col = -1;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (grid[r, c] == player)
                    {
                        // Check horizontally
                        if (c + 2 < Cols)
                        {
                            bool leftEmpty = c == 0 || grid[r, c - 1] == PlayerType.Empty;
                            bool rightEmpty = c + 3 >= Cols || grid[r, c + 3] == PlayerType.Empty;
                            if (((c != 4 && leftEmpty) && (c != 2 && rightEmpty)) && grid[r, c + 1] == player && grid[r, c + 2] == player)
                            {
                                _horizontalStreak3O = true;
                                row = r;
                                col = c + 2;
                                return true;
                            }
                        }

                        // Check vertically
                        if (r + 2 < Rows)
                        {
                            bool topEmpty = r == 0 || grid[r - 1, c] == PlayerType.Empty;
                            bool bottomEmpty = r + 3 >= Rows || grid[r + 3, c] == PlayerType.Empty;
                            if (((r != 4 && topEmpty) && (r != 2 && bottomEmpty)) && grid[r + 1, c] == player && grid[r + 2, c] == player)
                            {
                                _verticalStreak3O = true;
                                row = r + 2;
                                col = c;
                                return true;
                            }
                        }

                        // Check diagonally (tl-br)
                        if (r + 2 < Rows && c + 2 < Cols && r - 1 >= 0 && c - 1 >= 0 && r + 3 < Rows && c + 3 < Cols)
                        {
                            bool topLeftEmpty = grid[r - 1, c - 1] == PlayerType.Empty;
                            bool bottomRightEmpty = grid[r + 3, c + 3] == PlayerType.Empty;
                            if ((topLeftEmpty || bottomRightEmpty) && grid[r + 1, c + 1] == player && grid[r + 2, c + 2] == player)
                            {
                                _TLBRStreak3O = true;
                                row = r + 2;
                                col = c + 2;
                                return true;
                            }
                        }

                        // Check diagonally (tr-bl)
                        if (r + 2 < Rows && c - 2 >= 0 && r - 1 >= 0 && c + 1 < Cols && r + 3 < Rows && c - 3 >= 0)
                        {
                            bool topRightEmpty = grid[r - 1, c + 1] == PlayerType.Empty;
                            bool bottomLeftEmpty = grid[r + 3, c - 3] == PlayerType.Empty;
                            if ((topRightEmpty || bottomLeftEmpty) && grid[r + 1, c - 1] == player && grid[r + 2, c - 2] == player)
                            {
                                _TRBLStreak3O = true;
                                row = r + 2;
                                col = c - 2;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool StopStreak3(PlayerType[,] grid, PlayerType player, out int row, out int col)
        {
            row = -1;
            col = -1;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (grid[r, c] == player)
                    {
                        // Check horizontally
                        if (c + 2 < Cols)
                        {
                            bool leftEmpty = c == 0 || grid[r, c - 1] == PlayerType.Empty;
                            bool rightEmpty = c + 3 >= Cols || grid[r, c + 3] == PlayerType.Empty;
                            if ( ((c != 4 && leftEmpty) && (c != 2 && rightEmpty)) && grid[r, c + 1] == player && grid[r, c + 2] == player)
                            {
                                _horizontalStreakX = true;
                                row = r;
                                col = c + 2;
                                return true;
                            }
                        }

                        // Check vertically
                        if (r + 2 < Rows)
                        {
                            bool topEmpty = r == 0 || grid[r - 1, c] == PlayerType.Empty;
                            bool bottomEmpty = r + 3 >= Rows || grid[r + 3, c] == PlayerType.Empty;
                            if (((r != 4 && topEmpty) && (r != 2 && bottomEmpty)) && grid[r + 1, c] == player && grid[r + 2, c] == player)
                            {
                                _verticalStreakX = true;
                                row = r + 2;
                                col = c;
                                return true;
                            }
                        }

                        // Check diagonally (tl-br)
                        if (r + 2 < Rows && c + 2 < Cols && r - 1 >= 0 && c - 1 >= 0 && r + 3 < Rows && c + 3 < Cols)
                        {
                            bool topLeftEmpty = grid[r - 1, c - 1] == PlayerType.Empty;
                            bool bottomRightEmpty = grid[r + 3, c + 3] == PlayerType.Empty;
                            if ((topLeftEmpty || bottomRightEmpty) && grid[r + 1, c + 1] == player && grid[r + 2, c + 2] == player)
                            {
                                _TLBRStreakX = true;
                                row = r + 2;
                                col = c + 2;
                                return true;
                            }
                        }

                        // Check diagonally (tr-bl)
                        if (r + 2 < Rows && c - 2 >= 0 && r - 1 >= 0 && c + 1 < Cols && r + 3 < Rows && c - 3 >= 0)
                        {
                            bool topRightEmpty = grid[r - 1, c + 1] == PlayerType.Empty;
                            bool bottomLeftEmpty = grid[r + 3, c - 3] == PlayerType.Empty;
                            if ((topRightEmpty || bottomLeftEmpty) && grid[r + 1, c - 1] == player && grid[r + 2, c - 2] == player)
                            {
                                _TRBLStreakX = true;
                                row = r + 2;
                                col = c - 2;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }


        private void MakeMachineMove()
        {
            var emptyCells = new List<(int, int)>();

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (board[r, c] == PlayerType.Empty)
                    {
                        emptyCells.Add((r, c));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var random = new Random();
                var randomCell = emptyCells[random.Next(emptyCells.Count)];
                board[randomCell.Item1, randomCell.Item2] = PlayerType.O;
                markers.Add(new Player(PlayerType.O));

            }
        }

        // Checking for winner in PvP and PvM modes
        public PlayerType CheckForWinner()
        {
            PlayerType winner = PlayerType.Empty;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (board[r, c] != PlayerType.Empty)
                    {
                        // Check horizontally
                        if (c + 3 < Cols &&
                            board[r, c] == board[r, c + 1] &&
                            board[r, c] == board[r, c + 2] &&
                            board[r, c] == board[r, c + 3])
                        {
                            winner = board[r, c];
                            MarkWinningStreak(r, c, 0, 1);
                        }

                        // Check vertically
                        if (r + 3 < Rows &&
                            board[r, c] == board[r + 1, c] &&
                            board[r, c] == board[r + 2, c] &&
                            board[r, c] == board[r + 3, c])
                        {
                            winner = board[r, c];
                            MarkWinningStreak(r, c, 1, 0);
                        }

                        // Check diagonally (tl-br)
                        if (r + 3 < Rows && c + 3 < Cols &&
                            board[r, c] == board[r + 1, c + 1] &&
                            board[r, c] == board[r + 2, c + 2] &&
                            board[r, c] == board[r + 3, c + 3])
                        {
                            winner = board[r, c];
                            MarkWinningStreak(r, c, 1, 1);
                        }

                        // Check diagonally (tr-bl)
                        if (r + 3 < Rows && c - 3 >= 0 &&
                            board[r, c] == board[r + 1, c - 1] &&
                            board[r, c] == board[r + 2, c - 2] &&
                            board[r, c] == board[r + 3, c - 3])
                        {
                            winner = board[r, c];
                            MarkWinningStreak(r, c, 1, -1);
                        }
                    }
                }
            }

            return winner;
        }

        // Check for winner in 3P mode
        public PlayerType ThreePlayerWinnerCheck()
        {
            PlayerType winner3 = PlayerType.Empty;

            // Check horizontally
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols - 2; c++)
                {
                    if (board[r, c] != PlayerType.Empty &&
                        board[r, c] == board[r, c + 1] &&
                        board[r, c] == board[r, c + 2])
                    {
                        winner3 = board[r, c];
                        ThreePlayerMarkWinningStreak(r, c, 0, 1);
                    }
                }
            }

            // Check vertically
            for (int r = 0; r < Rows - 2; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (board[r, c] != PlayerType.Empty &&
                        board[r, c] == board[r + 1, c] &&
                        board[r, c] == board[r + 2, c])
                    {
                        winner3 = board[r, c];
                        ThreePlayerMarkWinningStreak(r, c, 1, 0);
                    }
                }
            }

            // Check diagonally (tl-br)
            for (int r = 0; r < Rows - 2; r++)
            {
                for (int c = 0; c < Cols - 2; c++)
                {
                    if (board[r, c] != PlayerType.Empty &&
                        board[r, c] == board[r + 1, c + 1] &&
                        board[r, c] == board[r + 2, c + 2])
                    {
                        winner3 = board[r, c];
                        ThreePlayerMarkWinningStreak(r, c, 1, 1);
                    }
                }
            }

            // Check diagonally (tr-bl)
            for (int r = 0; r < Rows - 2; r++)
            {
                for (int c = 2; c < Cols; c++)
                {
                    if (board[r, c] != PlayerType.Empty &&
                        board[r, c] == board[r + 1, c - 1] &&
                        board[r, c] == board[r + 2, c - 2])
                    {
                        winner3 = board[r, c];
                        ThreePlayerMarkWinningStreak(r, c, 1, -1);
                    }
                }
            }

            return winner3;
        }

        private void MarkWinningStreak(int startRow, int startCol, int rowDirection, int colDirection)
        {
            // Change the winning streak marker - set into "W"
            for (int i = 0; i < 4; i++)
            {
                int row = startRow + i * rowDirection;
                int col = startCol + i * colDirection;

                if (row >= 0 && row < Rows && col >= 0 && col < Cols)
                {
                    board[row, col] = PlayerType.W;
                }
            }
        }

        public void ThreePlayerMarkWinningStreak(int startRow, int startCol, int rowDirection, int colDirection)
        {
            // Change the winning streak marker - set into "W"
            for (int i = 0; i < 3; i++)
            {
                int row = startRow + i * rowDirection;
                int col = startCol + i * colDirection;

                if (row >= 0 && row < Rows && col >= 0 && col < Cols)
                {
                    board[row, col] = PlayerType.W;
                }
            }
        }


        // Adding methods to switch between game modes
        public void SwitchToPlayerVsMachineMode()
        {
            _vsMachineMode = true;
            _vsPlayerMode = false;
            _threePlayerMode = false;
            Reset();
        }

        public void SwitchToPlayerVsPlayerMode()
        {
            _vsMachineMode = false;
            _vsPlayerMode = true;
            _threePlayerMode = false;
            Reset();
        }

        public void SwitchToThreePlayerMode()
        {
            _vsMachineMode = false;
            _vsPlayerMode = false;
            _threePlayerMode = true;
            Reset();
        }

        // Update scoresheet method for PvP and PvM modes
        public void UpdateScores(PlayerType winner)
        {
            if (winner == PlayerType.X)
            {
                XScore++;
            }
            else if (winner == PlayerType.O)
            {
                OScore++;
            }
        }

        // Update scoresheet method for 3P mode
        public void UpdateScores3P(PlayerType winner3)
        {
            if (winner3 == PlayerType.X)
            {
                XScore++;
            }
            else if (winner3 == PlayerType.O)
            {
                OScore++;
            }
            else if (winner3 == PlayerType.Z)
            {
                ZScore++;
            }
        }

        public void Reset()
        {
            InitializeBoard();
            markers.Clear();

            _horizontalStreak2O = false;
            _verticalStreak2O = false;
            _TLBRStreak2O = false;
            _TRBLStreak2O = false;

            _horizontalStreak3O = false;
            _verticalStreak3O = false;
            _TLBRStreak3O = false;
            _TRBLStreak3O = false;

            _horizontalStreakX = false;
            _verticalStreakX = false;
            _TLBRStreakX = false;
            _TRBLStreakX = false;
        }
    }
}
