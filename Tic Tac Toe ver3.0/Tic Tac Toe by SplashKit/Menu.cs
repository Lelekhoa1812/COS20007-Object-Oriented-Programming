using SplashKitSDK;
using System;

namespace TicTacToe
{
    public class Menu
    {
        private Window _window;
        private Rectangle _PlayerVsMachineModeButton;
        private Rectangle _playerVsPlayerModeButton;
        private Rectangle _threePlayerModeButton;

        public Menu(int windowWidth, int windowHeight)
        {
            _window = new Window("Tic Tac Toe Menu", windowWidth, windowHeight);
            _PlayerVsMachineModeButton = new Rectangle()
            {
                X = windowWidth / 4,
                Y = windowHeight / 2 - 50,
                Width = windowWidth / 2,
                Height = 50
            };
            _playerVsPlayerModeButton = new Rectangle()
            {
                X = windowWidth / 4,
                Y = windowHeight / 2 + 10,
                Width = windowWidth / 2,
                Height = 50
            };
            _threePlayerModeButton = new Rectangle()
            {
                X = windowWidth / 4,
                Y = windowHeight / 2 + 70,
                Width = windowWidth / 2,
                Height = 50
            };
        }

        public void Draw()
        {
            _window.Clear(Color.Black);
            _window.DrawText("Player vs Machine Mode", Color.White, "Arial", 36, _PlayerVsMachineModeButton.X + 20, _PlayerVsMachineModeButton.Y + 10);
            _window.DrawRectangle(Color.White, _PlayerVsMachineModeButton);
            _window.DrawText("Player vs Player Mode", Color.White, "Arial", 36, _playerVsPlayerModeButton.X + 20, _playerVsPlayerModeButton.Y + 10);
            _window.DrawRectangle(Color.White, _playerVsPlayerModeButton);
            _window.DrawText("3 Player Mode", Color.White, "Arial", 36, _threePlayerModeButton.X + 20, _threePlayerModeButton.Y + 10);
            _window.DrawRectangle(Color.White, _threePlayerModeButton);
            _window.Refresh();
        }

        public bool IsPlayerVsMachineModeClicked()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            return mousePosition.X >= _PlayerVsMachineModeButton.X && mousePosition.X <= _PlayerVsMachineModeButton.X + _PlayerVsMachineModeButton.Width &&
                   mousePosition.Y >= _PlayerVsMachineModeButton.Y && mousePosition.Y <= _PlayerVsMachineModeButton.Y + _PlayerVsMachineModeButton.Height &&
                   SplashKit.MouseClicked(MouseButton.LeftButton);
        }

        public bool IsPlayerVsPlayerModeClicked()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            return mousePosition.X >= _playerVsPlayerModeButton.X && mousePosition.X <= _playerVsPlayerModeButton.X + _playerVsPlayerModeButton.Width &&
                   mousePosition.Y >= _playerVsPlayerModeButton.Y && mousePosition.Y <= _playerVsPlayerModeButton.Y + _playerVsPlayerModeButton.Height &&
                   SplashKit.MouseClicked(MouseButton.LeftButton);
        }

        public bool IsThreePlayerModeClicked()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            return mousePosition.X >= _threePlayerModeButton.X && mousePosition.X <= _threePlayerModeButton.X + _threePlayerModeButton.Width &&
                   mousePosition.Y >= _threePlayerModeButton.Y && mousePosition.Y <= _threePlayerModeButton.Y + _threePlayerModeButton.Height &&
                   SplashKit.MouseClicked(MouseButton.LeftButton);
        }

        public void Hide()
        {
            _window.Close();
        }
    }
}
