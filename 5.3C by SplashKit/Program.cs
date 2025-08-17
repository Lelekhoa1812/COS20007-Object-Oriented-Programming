using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace DrawingShape
{
    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }


        public static void Main()
        {
            Drawing drawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Rectangle;

            Window window = new Window("Shape Drawer", 800, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {


                    Shape newShape;
                    if (kindToAdd == ShapeKind.Circle)
                    {
                        MyCircle newCircle = new MyCircle();
                        newShape = newCircle;
                    }
                    else if (kindToAdd == ShapeKind.Line)
                    {
                        MyLine newLine = new MyLine();
                        newShape = newLine;
                    }
                    else
                    {
                        MyRectangle newRect = new MyRectangle();
                        newShape = newRect;
                    }

                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();
                    drawing.AddShape(newShape);

                }


                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    drawing.SelectedShapeAt(SplashKit.MousePosition());
                }


                 if (SplashKit.KeyTyped(KeyCode.BackspaceKey))
                 {
                    List<Shape> selectedShapes = drawing.SelectedShapes();
                    foreach (Shape s in selectedShapes)
                    {
                        drawing.RemoveShape(s);
                    }
                    //Console.WriteLine("Backspace Key: Delete Shape");
                 }

                  if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                  {
                        drawing.Background = SplashKit.RandomRGBColor(255);
                    //Console.WriteLine("Space Key: Change Background");

                  }
                  //Annotated for 5.3C 
                  if (SplashKit.KeyTyped(KeyCode.SKey))
                  {
                        drawing.Save("/Users/khoale/Desktop/Visual Code Saver/5.3C/TestSave.txt");
                  }
                  if (SplashKit.KeyReleased(KeyCode.OKey))
                  {
                      try
                      {
                          drawing.Load("/Users/khoale/Desktop/Visual Code Saver/5.3C/TestSave.txt");
                      }
                      catch (Exception e)
                      {
                          Console.Error.WriteLine("Error loading file while {0}", e.Message);
                      }

                  }

                  drawing.Draw();
                  SplashKit.RefreshScreen();
            }
            while (!window.CloseRequested);
        }
    }

}