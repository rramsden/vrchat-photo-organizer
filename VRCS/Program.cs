using System;
using System.IO;

namespace VRCScreenshot
{
    class Program
    {
        static void Reset(string currentDir)
        {
            foreach (string d in Directory.GetDirectories(currentDir))
            {
                foreach(string f in Directory.GetFiles(d))
                // Move all files back
                {
                    if (f.EndsWith(".png"))
                    {
                        string newPath = Path.Combine(currentDir, Path.GetFileName(f));
                        MoveFile(f, newPath);
                    }
                }

                // Remove directory
                if (Directory.GetFiles(d).Length == 0)
                {
                    Directory.Delete(d);
                }
            }

            Terminate();
        }
        static void Organize(string currentDir, bool byDate)
        {
            string[] fileEntries = Directory.GetFiles(currentDir);

            foreach (string path in fileEntries)
            {
                string fileName = Path.GetFileName(path);

                if ((fileName.StartsWith("VRChat_") || fileName.StartsWith("vrchat_")) && fileName.EndsWith(".png"))
                {
                    string[] segments = fileName.Split("_");
                    string date = segments[2];

                    if (!byDate)
                    {
                        date = date.Substring(0, 7);
                    }

                    string pathString = Path.Combine(currentDir, date);
                    string destFilepath = Path.Combine(pathString, fileName);

                    Directory.CreateDirectory(pathString);
                    MoveFile(path, destFilepath);
                }
            }

            Terminate();
        }

        static void MoveFile(string path, string destFilePath)
        {
            if (File.Exists(destFilePath))
            {
                Console.WriteLine("Skipping [file exists] {0}", destFilePath);
            }
            else
            {
                Console.WriteLine("Moving {0}", destFilePath);
                File.Move(path, destFilePath);
            }
        }

        static void Terminate()
        {
            Console.WriteLine("All done :3");
            Console.ReadKey();
        }
        
        static void Main(string[] args)
        {
            string currentDir = Directory.GetCurrentDirectory();

            string input = null;

            while (input != "1" && input != "2" && input != "3" && input != "")
            {
                Console.WriteLine("Let's organize your files!\n");
                Console.WriteLine("  (1) Organize by month");
                Console.WriteLine("  (2) Organize by day");
                Console.WriteLine("  (3) Restore my files! Grrrr!");
                Console.Write("\nEnter an option (default is 1): ");

                input = Console.ReadLine();
            }


            if (input != "3")
            {
                bool byDate = (input == "2");
                Organize(currentDir, byDate);
            }
            else
            {
                Reset(currentDir);
            }
        }
    }
}
