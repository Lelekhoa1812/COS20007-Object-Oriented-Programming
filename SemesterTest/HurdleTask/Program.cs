using System;
using System.Collections.Generic;

namespace HurdleTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // File System
            FileSystem fileSystem = new FileSystem();

            // Files in File System (not within folder)
            fileSystem.Add(new File("MyImage", 5342, "jpg"));
            fileSystem.Add(new File("MyText", 832, "txt"));

            // Folder (contains files) in File System 
            Folder fileFolder = new Folder("MyFolder");
            fileFolder.Add(new File("Save 1 - The Citadel", 2348, "data"));
            fileFolder.Add(new File("Save 2 - Artemis Tau", 6378, "data"));
            fileFolder.Add(new File("Save 3 - Serpent Nebula", 973, "data"));
            fileSystem.Add(fileFolder);

            // Folder within folder (contains files) in File System
            Folder outsideFolder = new Folder("OutsideFolder");
            Folder insideFolder = new Folder("InsideFolder");
            insideFolder.Add(new File("NewText", 100, "txt"));
            outsideFolder.Add(insideFolder);
            fileSystem.Add(outsideFolder);

            // Empty Folder (contains none) in File System
            Folder emptyFolder = new Folder("BlankFolder");
            fileSystem.Add(emptyFolder);

            //Print output
            fileSystem.PrintContents();
        }
    }
}
