using SplashKitSDK;
using System;

namespace TicTacToe
{
    public class WinnerWindow
    {
        private Window _winWindow;
        private Rectangle _OKButton;

        public WinnerWindow(int WinnerWindowWidth, int WinnerWindowHeight)
        {
            _winWindow = new Window("Winner Window", WinnerWindowWidth, WinnerWindowHeight);
            _OKButton = new Rectangle()
            {
                X = WinnerWindowWidth / 2 - 40,
                Y = WinnerWindowHeight / 2 + 40,
                Width = WinnerWindowWidth / 5,
                Height = WinnerWindowHeight / 6,
            };
        }

        public void Draw()
        {
            _winWindow.Clear(Color.Black);
            _winWindow.DrawRectangle(Color.White, _OKButton);
            _winWindow.DrawText("Okay", Color.PeachPuff, "Arial", 36, _OKButton.X + 20, _OKButton.Y + 10);

            _winWindow.Refresh();
        }

        public bool IsOKButtonClicked()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            return mousePosition.X >= _OKButton.X && mousePosition.X <= _OKButton.X + _OKButton.Width &&
                   mousePosition.Y >= _OKButton.Y && mousePosition.Y <= _OKButton.Y + _OKButton.Height &&
                   SplashKit.MouseClicked(MouseButton.LeftButton);
        }

        public void Hide()
        {
            _winWindow.Close();
        }
    }
}
