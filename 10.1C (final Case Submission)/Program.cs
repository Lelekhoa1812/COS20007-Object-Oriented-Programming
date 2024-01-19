namespace SwinAdventure
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string name;
            string description;
            Player player;

            //Direct paths
            Path path1;
            Path path2;
            Path path3;
            Path path4;

            // Indirect paths
            Path path5a;
            Path path6a;
            Path path7a;
            Path path8a;

            Path path5b;
            Path path6b;
            Path path7b;
            Path path8b;

            Console.WriteLine("Welcome to SwinAdventure!");

            Console.WriteLine("Enter Player Name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter Player Description:");
            description = Console.ReadLine();

            player = new Player(name, description);

            Item sword = new Item(new string[] { "sword" }, "sword", "a bronze sword");
            Item gem = new Item(new string[] { "gem" }, "ruby", "a shining gem");
            Item pc = new Item(new string[] { "pc" }, "computer", "a small computer");
            Item book = new Item(new string[] { "book" }, "novel", "a lifetime autobiography");
            Item fruit = new Item(new string[] { "fruit" }, "orange", "sweet and fresh");
            Item fish = new Item(new string[] { "fish" }, "salmon", "very fresh seafood");
            Item ticket = new Item(new string[] { "ticket" }, "ticket", "horror movie ticket");
            Item popcorn = new Item(new string[] { "popcorn" }, "popcorn", "cheesy or caramel");

            Bag bag = new Bag(new string[] { "bag" }, "backpack", "a big bag");

            Location home = new Location(new string[] { "home" }, "home", "home sweet home", "west, northwest and southwest");
            Location school = new Location(new string[] { "school" }, "school", "a study time", "east, northeast and southeast");
            Location market = new Location(new string[] { "market" }, "market", "fresh fruits here", "north, northwest and northeast");
            Location cinema = new Location(new string[] { "cinema" }, "cinema", "late service tonight", "south, southwest and southeast");

            // Direct paths
            path1 = new Path(new string[] { "east" }, "east direction", "bus stop", school, home);
            path2 = new Path(new string[] { "west" }, "west direction", "bus stop", home, school);
            path3 = new Path(new string[] { "north" }, "north direction", "grocery store", market, cinema);
            path4 = new Path(new string[] { "south" }, "south direction", "grocery store", cinema, market);

            // Indirect paths
            path5a = new Path(new string[] { "northeast" }, "north-east direction", "greeny park", school, cinema);
            path5b = new Path(new string[] { "northeast" }, "north-east direction", "asian restaurant", market, home);

            path6a = new Path(new string[] { "northwest" }, "north-west direction", "fortress arcade", home, cinema);
            path6b = new Path(new string[] { "northwest" }, "north-west direction", "public library", market, school);

            path7a = new Path(new string[] { "southeast" }, "south-east direction", "public library", school, market);
            path7b = new Path(new string[] { "southeast" }, "south-east direction", "fortress arcade", cinema, home);

            path8a = new Path(new string[] { "southwest" }, "south-west direction", "asian restaurant", home, market);
            path8b = new Path(new string[] { "southwest" }, "south-west direction", "greeny park", cinema, school);

            player.Inventory.Put(bag);
            player.Inventory.Put(gem);
            player.Inventory.Put(sword);
            player.Inventory.Put(pc);

            bag.Inventory.Put(sword);
            bag.Inventory.Put(pc);

            player.Location = home;

            home.Inventory.Put(sword);
            home.Inventory.Put(gem);

            school.Inventory.Put(book);
            school.Inventory.Put(pc);

            cinema.Inventory.Put(popcorn);
            cinema.Inventory.Put(ticket);

            market.Inventory.Put(fish);
            market.Inventory.Put(fruit);

            school.AddPath(path1);
            school.AddPath(path5a);
            school.AddPath(path7a);

            home.AddPath(path2);
            home.AddPath(path6a);
            home.AddPath(path8a);

            market.AddPath(path3);
            market.AddPath(path5b);
            market.AddPath(path6b);

            cinema.AddPath(path4);
            cinema.AddPath(path7b);
            cinema.AddPath(path8b);


            string cmd;

            LookCommand look = new LookCommand();
            MoveCommand move = new MoveCommand();

            while (true)
            {
                Console.WriteLine("Enter a Command: ");
                cmd = Console.ReadLine();

                //Using for 9.2C only
                /*if (cmd == "quit")
                {
                    quit = true;
                    moved = false;
                    looked = false;
                }

                else if (cmd == "move")
                {
                    moved = true;
                    looked = false;
                    quit = false;
                }

                else if (cmd == "look")
                {
                    looked = true;
                    moved = false;
                    quit = false;
                }*/

                string[] exeCommand = cmd.ToLower().Split(' ');
                if (exeCommand[0] == "quit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(new CommandProcessor().ExecuteCommand(player, exeCommand));
                }
            }
        }
    }
}