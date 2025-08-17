using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace DrawingShape
{
    public class Drawing
    {
        private readonly List<Shape> _shapes;
        private Color _background;

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        public Drawing() : this(Color.White)
        {
        }

        public Color Background
        {
            get
            {return _background;}
            set
            {_background = value;}
        }

        public int ShapeCount
        {
            get {return _shapes.Count;}
        }
        public List<Shape> SelectedShapes()
        {
            List<Shape> selectedShapes = new List<Shape>();
            foreach (Shape s in _shapes)
            {
                if (s.Selected)
                {
                    selectedShapes.Add(s);
                }
            }
            return selectedShapes;
        }

        public void SelectedShapeAt(Point2D pt)
        {
            foreach (Shape s in _shapes)
            {
                if (s.IsAt(pt))
                {s.Selected = true;}
                else
                {s.Selected = false;}
            }
        }

        public void ChangingShapeColor()
        {
            foreach (Shape s in _shapes)
            {
                if (s.Selected)
                {
                    s.COLOR = Color.RandomRGB(255);
                }
            }
        }

        public void AddShape(Shape s)
        {
            _shapes.Add(s);
        }

        public void RemoveShape(Shape s)
        {
            _shapes.Remove(s);
 
        }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);

            foreach (Shape s in _shapes)
            {
                s.Draw();
            }
        }

        //Annotated for 5.3C 
        public void Save(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteColor(Background);
            writer.WriteLine(ShapeCount);
            foreach (Shape s in _shapes)
            {
                s.SaveTo(writer);
            }
            writer.Close();

        }

        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            try
            {
                int count;
                Shape s;
                string kind;

                Background = reader.ReadColor();
                count = reader.ReadInteger();
                _shapes.Clear();
                for (int i = 0; i < count; i++)
                {
                    kind = reader.ReadLine();
                    switch (kind)
                    {
                        case "Rectangle":
                            s = new MyRectangle();
                            break;
                        case "Circle":
                            s = new MyCircle();
                            break;
                        case "Line":
                            s = new MyLine();
                            break;
                        default:
                            throw new InvalidDataException("Unknown shape kind: " + kind);
                    }
                    s.LoadFrom(reader);
                    AddShape(s);
                }
            }
            finally
            {
                reader.Close();
            }

        }

    }
}
