using System;
namespace HurdleTask
{
    class File : Thing
    {
        private int _size;
        private string _extension;

        public File(string name, int size, string extension) : base(name)
        {
            _size = size;
            _extension = extension;
        }

        //Size
        public override int Size()
        {
            return _size;
        }

        public string Extension
        {
            get { return _extension; }
        }


        //Print structure
        public override void Print()
        {
            Console.WriteLine($"File '{_name}.{_extension}' -- {_size} bytes");
        }
    }
}

