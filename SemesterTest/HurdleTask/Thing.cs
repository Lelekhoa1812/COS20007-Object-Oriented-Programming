using System;
namespace HurdleTask
{
    abstract class Thing
    {
        protected string _name;

        public Thing(string name)
        {
            _name = name;
        }

        public abstract int Size();
        public abstract void Print();

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}

