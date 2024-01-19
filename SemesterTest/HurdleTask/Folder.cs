using System;
namespace HurdleTask
{
    class Folder : Thing
    {
        private List<Thing> _contents;

        public Folder(string name) : base(name)
        {
            _contents = new List<Thing>();
        }

        //Add
        public void Add(Thing toAdd)
        {
            _contents.Add(toAdd);
        }

        //Size
        public override int Size()
        {
            int size = 0;
            foreach (Thing item in _contents)
            {
                size += item.Size();
            }
            return size;
        }

        //Print structure
        public override void Print()
        {
            if (_contents.Count == 0)
            {
                Console.WriteLine($"The folder '{_name}' is empty!");
            }
            else
            {
                Console.WriteLine($"The folder '{_name}' contains {Size()} bytes total:");
                foreach (Thing item in _contents)
                {
                    item.Print();
                }
            }
        }
    }
}

