using System;
namespace RandomGeneration
{
	public class Options
	{
		private Random rand;
        public int Value { get; private set; }
		private bool tog = false;

        public Options(int val)
		{
			rand = new Random();
			Value = val;
		}

		public void RandomRoll()
		{
            Value = rand.Next(1, 7);
        }

		public void Choosen(int val)
		{
			if (!tog || tog)
			{
				if (val == 1)
				{
					Console.WriteLine("An Nha");
                    tog = true;
				}
                else if (val == 2)
                {
                    Console.WriteLine("Pho Ganh");
                    tog = true;
                }
                else if (val == 3)
                {
                    Console.WriteLine("Dragon HP");
                    tog = true;
                }
                else if (val == 4)
                {
                    Console.WriteLine("Do Thai");
                    tog = true;
                }
                else if (val == 5)
                {
                    Console.WriteLine("BBQ");
                    tog = true;
                }
                else if (val == 6)
                {
                    Console.WriteLine("Doodee");
                    tog = true;
                }
            }
		}

    }
}

