using System;
namespace RandomGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Options opt = new Options(0);
            bool eg = false;

            while (!eg)
            {
                opt.RandomRoll();
                opt.Choosen(opt.Value);
            }
        }
    }
}