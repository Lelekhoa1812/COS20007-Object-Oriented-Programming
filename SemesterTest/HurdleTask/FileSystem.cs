using System;
namespace HurdleTask
{
    class FileSystem
    {
        private List<Thing> _contents;

        public FileSystem()
        {
            _contents = new List<Thing>();
        }

        //Add
        public void Add(Thing toAdd)
        {
            _contents.Add(toAdd);
        }

        //Print structure
        public void PrintContents()
        {
            Console.WriteLine("This file system contains:");
            foreach (Thing item in _contents)
            {
                item.Print();
            }
        }
    }
}

